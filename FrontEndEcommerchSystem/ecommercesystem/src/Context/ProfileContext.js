import { createContext, useContext, useState } from "react";

const ProfileContext = createContext();

export function ProfileProvider({ children }) {
  const [stats, setStats] = useState(null);
  return (
    <ProfileContext.Provider value={{ stats, setStats }}>
      {children}
    </ProfileContext.Provider>
  );
}

export const useProfile = () => useContext(ProfileContext);
