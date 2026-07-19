import { NavLink } from "react-router-dom";
import "./SideBarProfile.css";
import {
  faBagShopping,
  faGear,
  faHeadset,
  faHeart,
  faRightFromBracket,
} from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import useLogout from "../../Features/Auth/hooks/useLogout";

export default function SideBarProfile() {
  const logout = useLogout();
  return (
    <div className="profile-sidebar">
      <NavLink to="/Profile" end className="profile-item">
        <div className="d-flex align-items-center">
          <FontAwesomeIcon icon={faBagShopping} />
          <span>Orders</span>
        </div>

        <i className="fa-solid fa-chevron-right"></i>
      </NavLink>

      <NavLink to="/Profile/wishlist" className="profile-item">
        <div className="d-flex align-items-center">
          <FontAwesomeIcon icon={faHeart} />
          <span>Wishlist</span>
        </div>
      </NavLink>

      <NavLink to="/Profile/setting" className="profile-item">
        <div className="d-flex align-items-center">
          <FontAwesomeIcon icon={faGear} />
          <span>Settings</span>
        </div>
      </NavLink>

      <NavLink to="/Profile/support" className="profile-item">
        <div className="d-flex align-items-center">
          <FontAwesomeIcon icon={faHeadset} />
          <span>Support</span>
        </div>
      </NavLink>

      <hr />

      <button onClick={logout} className="logout-btn">
        <FontAwesomeIcon icon={faRightFromBracket} />
        Log Out
      </button>

      <div className="upgrade-card mt-4">
        <h5>Upgrade to Pro</h5>

        <p>Get exclusive access to pre-release components.</p>

        <button className="btn btn-light fw-bold">LEARN MORE</button>

        <i className="fa-regular fa-gem diamond"></i>
      </div>
    </div>
  );
}
