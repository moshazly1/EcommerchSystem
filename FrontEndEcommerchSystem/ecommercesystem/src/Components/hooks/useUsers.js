import { useEffect, useState } from "react";
import { basURL, USERID } from "../../API/api";
import useAxiosPrivate from "../../Features/Auth/hooks/useAxiosPrivate";

const useUsersID = (userId) => {
  const [user, setUser] = useState(null);
  const axiosPrivate = useAxiosPrivate();

  useEffect(() => {
    const getUser = async () => {
      try {
        const response = await axiosPrivate.get(`${basURL}${USERID}/${userId}`);

        setUser(response.data.data);
      } catch (err) {
        console.error(err);
      }
    };

    if (userId) {
      getUser();
    }
  }, [userId, axiosPrivate]);

  return user;
};

export default useUsersID;
