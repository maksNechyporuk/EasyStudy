import React, { useState, useEffect } from "react";
import { useTranslation } from "react-i18next";
import { useHistory } from "react-router";
import GroupAdminPage from "../SuperAdmin/Group/Group";
import TeacherAdminPage from "../SuperAdmin/Teacher/Teacher";
import Sidebar from "../../components/Sidebar/Sidebar";
import "../../styles.css";
import { getRegisterPageRoute, getCreateGroupRoute } from "../../routes/routes";
import "./MainPage.css";
import StudentAdminPage from "../SuperAdmin/Student/Student";
import StudentPage from "../Manager/Student/Student";
import GroupPage from "../Manager/Group/Group";
import TeacherPage from "../Manager/Teacher/Teacher";
import { loginByJWT } from "../../services/jwtService";

const MainPage = () => {
  let emptyGroup = {
    id: null,
    name: "",
    teacher: "",
    teacherId: "",
  };
  useEffect(() => {
    if (localStorage.authToken && localStorage.refreshToken) {
      let data = {
        token: localStorage.authToken,
        refreshToken: localStorage.refreshToken,
      };
      loginByJWT(data);
      setInProgress(false);
    } else {
      history.push(getRegisterPageRoute());
    }
  }, []);
  const history = useHistory();
  const [inProgress, setInProgress] = useState(true);

  const [pageState, setPageState] = useState({
    isShowManageGroup: false,
    isShowManageTeacher: false,
    isShowManageStudent: false,
    isShowManageAdminGroup: false,
    isShowManageAdminTeacher: false,
    isShowManageAdminStudent: false,
    isShowManageAdminSchool: false,
  });
  return (
    <>
      <Sidebar state={pageState} setState={setPageState} />
      {pageState.isShowManageAdminGroup ? (
        <>
          <GroupAdminPage />
        </>
      ) : (
        <div></div>
      )}
      {pageState.isShowManageAdminTeacher ? (
        <>
          <TeacherAdminPage />
        </>
      ) : (
        <div></div>
      )}
      {pageState.isShowManageAdminStudent ? (
        <>
          <StudentAdminPage />
        </>
      ) : (
        <div></div>
      )}
      {pageState.isShowManageGroup ? (
        <>
          <GroupPage />
        </>
      ) : (
        <div></div>
      )}
      {pageState.isShowManageTeacher ? (
        <>
          <TeacherPage />
        </>
      ) : (
        <div></div>
      )}
      {pageState.isShowManageStudent ? (
        <>
          <StudentPage />
        </>
      ) : (
        <div></div>
      )}
      {pageState.isShowManageAdminSchool ? (
        <>School managerascasdcsadc</>
      ) : (
        <div></div>
      )}
    </>
  );
};

export default MainPage;
