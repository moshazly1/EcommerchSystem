import { useEffect, useState } from "react";
import useAuth from "../../Features/Auth/hooks/useAuth";
import {
  ACCOUNTACTIVATION,
  basURL,
  EMAILDIGEST,
  TWOFACTOORISENABLE,
  UPDATEUSER,
  USER,
  USERID,
} from "../../API/api";
import useAxiosPrivate from "../../Features/Auth/hooks/useAxiosPrivate";
import { jwtDecode } from "jwt-decode";
import Setting from "../../Pages/DashBoardProfile/Setting";

export default function useSettings() {
  const { auth } = useAuth();
  const axiosPrivate = useAxiosPrivate();
  const [data, setData] = useState({
    displayName: "",
    email: "",
    phoneNumber: "",
  });
  const [emailDigest, setEmailDigest] = useState(true);
  const [AccountActeve, setAccountActive] = useState(true);
  const [TwoFactorAuth, set2FactorAuth] = useState(true);

  const toggelAccountActive = async (value) => {
    setAccountActive(value);
    try {
      const res = await axiosPrivate.put(`${basURL}${ACCOUNTACTIVATION}`, {
        accountActivity: value,
      });
      console.log(res);
    } catch (err) {
      console.log(err);
    }
  };
  const ButtomIsEnable = async (value) => {
    set2FactorAuth(value);
    try {
      const res = await axiosPrivate.put(`${basURL}${TWOFACTOORISENABLE}`, {
        IsTwoFactorEnabled: value,
      });
      console.log(res);
    } catch (err) {
      console.log(err);
    }
  };
  const toggleEmailDigest = async (value) => {
    setEmailDigest(value);
    try {
      await axiosPrivate.put(`${basURL}${EMAILDIGEST}`, {
        emailDigest: value,
      });
    } catch (err) {
      console.log(err);
    }
  };

  useEffect(() => {
    const fetchUser = async () => {
      try {
        const res = await axiosPrivate.get(`${basURL}api/User/me`);
        setData({
          displayName: res.data.data.fullName,
          email: res.data.data.email,
          phoneNumber: res.data.data.phoneNumber || "",
          timezone: Intl.DateTimeFormat().resolvedOptions().timeZone,
        });

        console.log("User data:", res.data.data);
        console.log("AccountActivity:", res.data.data.accountActivity);
        setAccountActive(res.data.data.accountActivity);
        setEmailDigest(res.data.data.emailDigest);
        set2FactorAuth(res.data.data.isTwoFactorAuth);
        console.log();
      } catch (err) {
        console.log(err);
      }
    };
    fetchUser();
  }, []);

  const UpdateUser = async () => {
    try {
      await axiosPrivate.put(`${basURL}${UPDATEUSER}`, {
        fullname: data.displayName,
        phoneNumber: data.phoneNumber,
      });
      console.log("User updated successfully");
    } catch (err) {
      console.log(err);
    }
  };
  return {
    data,
    setData,
    UpdateUser,
    emailDigest,
    TwoFactorAuth,
    ButtomIsEnable,
    toggleEmailDigest,
    toggelAccountActive,
    AccountActeve,
  };
}
