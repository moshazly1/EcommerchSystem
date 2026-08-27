import { useState, useEffect, useRef } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { VerifyTwoFactorCode, ResendTwoFactorcode } from "../Service/authApi";
import useAuth from "./useAuth";

const CODE_LENGTH = 6;

const getRemainingSeconds = (expiresAt) => {
  if (!expiresAt) return 0;

  const difference = new Date(expiresAt).getTime() - Date.now();

  return Math.max(0, Math.floor(difference / 1000));
};

export default function useVerificationCode() {
  const { setAuth } = useAuth();
  const location = useLocation();
  const navigate = useNavigate();

  // أول محاولة: من location.state (جاي من صفحة الـ Login مباشرة)
  // لو مش موجود (يعني حصل refresh)، هنجيبه من sessionStorage
  const stateEmail = location.state?.email;
  const stateExpiresAt = location.state?.expiresAt;

  const email = stateEmail || sessionStorage.getItem("twoFactorEmail") || "";

  const initialExpiresAt =
    stateExpiresAt || sessionStorage.getItem("twoFactorExpiresAt") || null;

  // نحفظهم في sessionStorage أول ما يوصلوا (مرة واحدة بس، مش كل render)
  useEffect(() => {
    if (stateEmail) {
      sessionStorage.setItem("twoFactorEmail", stateEmail);
    }
    if (stateExpiresAt) {
      sessionStorage.setItem("twoFactorExpiresAt", stateExpiresAt);
    }
  }, [stateEmail, stateExpiresAt]);

  // لو مفيش إيميل خالص (لا من location ولا من sessionStorage) يبقى مفيش داعي المستخدم يكون في الصفحة دي أصلاً
  useEffect(() => {
    if (!email) {
      navigate("/login", { replace: true });
    }
  }, [email, navigate]);

  const [expiresAt, setExpiresAt] = useState(initialExpiresAt);

  const [secondsLeft, setSecondsLeft] = useState(() =>
    getRemainingSeconds(expiresAt),
  );

  const [code, setCode] = useState(Array(CODE_LENGTH).fill(""));

  const [loading, setLoading] = useState(false);
  const [resendLoading, setResendLoading] = useState(false);
  const [message, setMessage] = useState("");
  const [isValid, setIsValid] = useState(false);

  const inputsRef = useRef([]);

  useEffect(() => {
    if (!expiresAt) return;

    const interval = setInterval(() => {
      const remaining = getRemainingSeconds(expiresAt);

      setSecondsLeft(remaining);

      if (remaining <= 0) {
        clearInterval(interval);
      }
    }, 1000);

    return () => clearInterval(interval);
  }, [expiresAt]);

  // =========================
  // Format Timer
  // =========================
  const formatTime = (totalSeconds) => {
    const minutes = Math.floor(totalSeconds / 60);
    const seconds = totalSeconds % 60;

    return `${minutes}:${seconds.toString().padStart(2, "0")}`;
  };

  // =========================
  // Input Change
  // =========================
  const handleChange = (value, index) => {
    if (!/^[0-9]?$/.test(value)) {
      return;
    }

    const newCode = [...code];

    newCode[index] = value;

    setCode(newCode);
    setMessage("");
    setIsValid(false);

    if (value && index < CODE_LENGTH - 1) {
      inputsRef.current[index + 1]?.focus();
    }
  };

  // =========================
  // Backspace
  // =========================
  const handleKeyDown = (e, index) => {
    if (e.key === "Backspace" && !code[index] && index > 0) {
      inputsRef.current[index - 1]?.focus();
    }
  };

  // =========================
  // Verify Code
  // =========================
  const handleNext = async () => {
    const fullCode = code.join("");

    if (fullCode.length !== CODE_LENGTH) {
      setMessage("Please enter the complete verification code.");
      setIsValid(false);
      return;
    }

    if (secondsLeft <= 0) {
      setMessage("Verification code has expired.");
      setIsValid(false);
      return;
    }

    setLoading(true);
    setMessage("");
    setIsValid(false);

    try {
      const res = await VerifyTwoFactorCode(email, fullCode);

      console.log("2FA response:", res);

      setIsValid(true);

      setMessage(res?.mesage || res?.message || "Verification successful.");

      if (res?.isAuthentication) {
        sessionStorage.removeItem("twoFactorEmail");
        sessionStorage.removeItem("twoFactorExpiresAt");

        setAuth({
          accessToken: res.accessToken,
          user: res,
          roles: res.role,
        });

        if (Number(res.role) === 1) {
          navigate("/dashboard");
        } else if (Number(res.role) === 2) {
          navigate("/");
        } else {
          navigate("/unauthorized");
        }
      }
    } catch (err) {
      console.log("Verification error:", err);

      setIsValid(false);

      if (err.response?.data) {
        const errorData = err.response.data;

        setMessage(
          errorData?.mesage ||
            errorData?.message ||
            errorData ||
            "Invalid verification code.",
        );
      } else {
        setMessage("Verification failed. Please try again.");
      }
    } finally {
      setLoading(false);
    }
  };

  // =========================
  // Resend - دلوقتي بتنده على الـ API فعليًا
  // =========================
  const handleResend = async () => {
    // منع الضغط وهي لسه بتبعت، أو قبل ما الوقت يخلص
    if (resendLoading || secondsLeft > 0) return;

    setResendLoading(true);
    setMessage("");
    setIsValid(false);

    try {
      const res = await ResendTwoFactorcode(email);

      console.log("Resend response:", res);

      // نحدث وقت الانتهاء الجديد -> ده هيشغل العداد تاني من الأول
      setExpiresAt(res.twoFactorCodeExpiresAt);
      sessionStorage.setItem("twoFactorExpiresAt", res.twoFactorCodeExpiresAt);

      // نفضي الخانات ونرجع الفوكس لأول خانة
      setCode(Array(CODE_LENGTH).fill(""));
      inputsRef.current[0]?.focus();

      setMessage(res?.mesage || "A new code has been sent to your email.");
    } catch (err) {
      console.log("Resend error:", err);

      if (err.response?.data) {
        const errorData = err.response.data;
        setMessage(
          typeof errorData === "string"
            ? errorData
            : errorData?.mesage || "Failed to resend code.",
        );
      } else {
        setMessage("Failed to resend code. Please try again.");
      }
    } finally {
      setResendLoading(false);
    }
  };

  // =========================
  // Mask Email
  // =========================
  const maskEmail = (email) => {
    if (!email || !email.includes("@")) {
      return email;
    }

    const [name, domain] = email.split("@");

    if (!name) {
      return `****@${domain}`;
    }

    return `${name.substring(0, 2)}****@${domain}`;
  };

  return {
    email,
    code,
    secondsLeft,
    loading,
    resendLoading,
    message,
    isValid,
    inputsRef,
    CODE_LENGTH,
    formatTime,
    maskEmail,
    handleChange,
    handleKeyDown,
    handleNext,
    handleResend,
  };
}
