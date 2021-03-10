import React, { useState, useEffect } from "react";
import { useTranslation } from "react-i18next";
import { useHistory } from "react-router";
import { ProSidebar, Menu, MenuItem, SubMenu } from "react-pro-sidebar";
import jwt from "jsonwebtoken";
import { FaChalkboardTeacher, FaChild, FaBook } from "react-icons/fa";
import menuImage from "../../images/menuBack.jpg";
import "react-pro-sidebar/dist/css/styles.css";

const Sidebar = ({ state, setState }) => {
  const history = useHistory();
  const [isCollapsed, setIsCollapsed] = useState(true);
  const { t } = useTranslation();
  console.log("state=>", state);
  useEffect(() => {}, []);

  const adminMenu = () => {
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
      </>
    );
  };

  const renderMenu = () => {
    if (localStorage.authToken && localStorage.refreshToken) {
      var user = jwt.decode(localStorage.authToken);
      if (user.roles == "Admin") {
        return adminMenu();
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
    </ProSidebar>
  );
};

export default Sidebar;
