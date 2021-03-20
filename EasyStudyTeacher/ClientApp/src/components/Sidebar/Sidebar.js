import React, { useState, useEffect } from "react";
import { useTranslation } from "react-i18next";
import i18n from "../../locale/resourses";

import { useHistory } from "react-router";
import { ProSidebar, Menu, MenuItem, SidebarFooter } from "react-pro-sidebar";
import jwt from "jsonwebtoken";
import { FaChalkboardTeacher, FaChild, FaBook } from "react-icons/fa";
import { CgLogOut } from "react-icons/cg";
import menuImage from "../../images/menuBack.jpg";
import { logout } from "../../services/jwtService";
import { getRegisterPageRoute } from "../../routes/routes";
import "../../styles.css";

const Sidebar = ({ state, setState }) => {
  const history = useHistory();
  const [isCollapsed, setIsCollapsed] = useState(true);
  const { t } = useTranslation();
  const [isActive, setActive] = useState(true);

  useEffect(() => {
    if (localStorage.authToken && localStorage.refreshToken) {
      var user = jwt.decode(localStorage.authToken);
      if (user.roles == "SuperAdmin") {
        setState({ isShowManageAdminGroup: true });
      } else if (user.roles == "Admin") {
        setState({ isShowManageGroup: true });
      }
    }
    //console.log("state=>", state);
  }, []);

  const changeLanguage = (lng) => {
    i18n.changeLanguage(lng);
  };
  const managerMenu = () => {
    return (
      <>
        <MenuItem
          onClick={() => {}}
          active={state.isShowManageGroup}
          onClick={() => {
            setState({ isShowManageGroup: true });
          }}
          icon={<FaBook />}
        >
          {t("Admin.GroupManagement")}
        </MenuItem>
        <MenuItem
          active={state.isShowManageTeacher}
          onClick={() => {
            setState({ isShowManageTeacher: true });
          }}
          icon={<FaChalkboardTeacher />}
        >
          {t("Admin.GroupTeacher")}
        </MenuItem>
        <MenuItem
          active={state.isShowManageStudent}
          onClick={() => {
            setState({ isShowManageStudent: true });
          }}
          icon={<FaChild />}
        >
          {t("Admin.GroupStudent")}
        </MenuItem>
        <MenuItem
          active={state.isShowManageStudent}
          onClick={() => {
            logout();
            history.push(getRegisterPageRoute());
          }}
          icon={<CgLogOut />}
        >
          {t("Common.logOut")}
        </MenuItem>
      </>
    );
  };
  const adminMenu = () => {
    return (
      <>
        <MenuItem
          onClick={() => {}}
          active={state.isShowManageAdminGroup}
          onClick={() => {
            setState({ isShowManageAdminGroup: true });
          }}
          icon={<FaBook />}
        >
          {t("Admin.GroupManagement")}
        </MenuItem>
        <MenuItem
          active={state.isShowManageAdminTeacher}
          onClick={() => {
            setState({ isShowManageAdminTeacher: true });
          }}
          icon={<FaChalkboardTeacher />}
        >
          {t("Admin.GroupTeacher")}
        </MenuItem>
        <MenuItem
          active={state.isShowManageAdminStudent}
          onClick={() => {
            setState({ isShowManageAdminStudent: true });
          }}
          icon={<FaChild />}
        >
          {t("Admin.GroupStudent")}
        </MenuItem>
        <MenuItem
          onClick={() => {
            logout();
            history.push(getRegisterPageRoute());
          }}
          icon={<CgLogOut />}
        >
          {t("Common.logOut")}
        </MenuItem>
      </>
    );
  };

  const renderMenu = () => {
    if (localStorage.authToken && localStorage.refreshToken) {
      var user = jwt.decode(localStorage.authToken);
      if (user.roles == "SuperAdmin") {
        return adminMenu();
      } else if (user.roles == "Admin") {
        return managerMenu();
      }
    }
  };

  return (
    <ProSidebar
      style={{ position: "fixed" }}
      collapsed={isCollapsed}
      image={menuImage}
      onMouseOver={() => {
        setIsCollapsed(false);
      }}
      onMouseOut={() => {
        setIsCollapsed(true);
      }}
    >
      <Menu iconShape="square">{renderMenu()}</Menu>
      <SidebarFooter>
        <div className="language">
          <span
            className={isActive ? "active" : null}
            onClick={() => {
              setActive(true);
              changeLanguage("en");
            }}
          >
            EN
          </span>{" "}
          <span
            className={isActive ? null : "active"}
            onClick={() => {
              changeLanguage("uk");
              setActive(false);
            }}
          >
            UA
          </span>
        </div>
      </SidebarFooter>
    </ProSidebar>
  );
};

export default Sidebar;
