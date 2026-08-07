import { Outlet } from "react-router-dom";
import SideBarProfile from "./DashBoardProfile/SideBarBrofile";
import { ProfileProvider, useProfile } from "../Context/ProfileContext";
import UserID from "./UserID";

function ProfileContent() {
  const { stats } = useProfile();
  return (
    <div style={{ backgroundColor: "var(--brand-main)" }}>
      <UserID stats={stats} />
      <div className="container py-5">
        <div className="row">
          <div className="col-lg-3 col-md-4 mb-4">
            <SideBarProfile />
          </div>
          <div className="col-lg-9 col-md-8">
            <Outlet />
          </div>
        </div>
      </div>
    </div>
  );
}

export default function Profile() {
  return (
    <ProfileProvider>
      <ProfileContent />
    </ProfileProvider>
  );
}
