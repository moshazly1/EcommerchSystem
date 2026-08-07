import { useEffect, useState } from "react";
import useAuth from "../../Features/Auth/hooks/useAuth";
import { basURL, UPDATEUSER, USER, USERID } from "../../API/api";
import useAxiosPrivate from "../../Features/Auth/hooks/useAxiosPrivate";
import { jwtDecode } from "jwt-decode";

export default function useSettings() {
  const { auth } = useAuth();
  const axiosPrivate = useAxiosPrivate();

  const [data, setData] = useState({
    displayName: "",
    email: "",
    phoneNumber: "",
  });

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
  return { data, setData, UpdateUser };
}
