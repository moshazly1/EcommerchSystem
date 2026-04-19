import { useEffect, useState } from "react";
import { basURL, USER } from "../API/api";
import useAxiosPrivate from "../Features/Auth/hooks/useAxiosPrivate";
import useRefreshToken from "../Features/Auth/hooks/useRefreshToken";

const Users = () => {
  const [User, setUser] = useState([]);
  const axiosPrivate = useAxiosPrivate();
  const refresh = useRefreshToken();
  useEffect(() => {
    let isMounted = true;
    const controller = new AbortController();
    const GetUsers = async () => {
      try {
        const response = await axiosPrivate.get(`${basURL}${USER}`, {
          //   signal: controller.signal,
        });
        console.log(response.data.data);
        isMounted && setUser(response.data.data);
      } catch (err) {
        console.error(err);
      }
    };
    GetUsers();
    return () => {
      isMounted = false;
      controller.abort();
    };
  }, []);

  return (
    <article>
      <h2> User List </h2>
      {User?.length ? (
        <ul>
          {User.map((user, i) => (
            <li key={i}>{user?.fullName}</li>
          ))}
        </ul>
      ) : (
        <p>No user To desplay </p>
      )}
      <button onClick={() => refresh()}> refresh </button>
    </article>
  );
};

export default Users;
