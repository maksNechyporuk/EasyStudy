import React, { useState } from "react";
import { useTranslation } from "react-i18next";
import { useHistory } from "react-router";
import GroupPage from "../Group/Group";
import TeacherPage from "../Teacher/Teacher";
import Sidebar from "../../components/Sidebar/Sidebar";
import "react-pro-sidebar/dist/css/styles.css";

import "./MainPage.css";

const MainPage = () => {
  let emptyGroup = {
    id: null,
    name: "",
    teacher: "",
    teacherId: "",
  };
  const history = useHistory();
  const [pageState, setPageState] = useState({
    isShowManageGroup: true,
    isShowManageTeacher: false,
    isShowManageStudent: false,
  });
  return (
    <>
      <Sidebar state={pageState} setState={setPageState} />
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
      {pageState.isShowManageStudent ? <>isShowManageStudent</> : <div></div>}
    </>
  );
};

export default MainPage;
