import React, { useState, useEffect, useRef } from "react";
import { useTranslation } from "react-i18next";
import {
  getRegisterPageRoute,
  getCreateGroupRoute,
} from "../../../routes/routes";
import { useHistory } from "react-router";
import { loginByJWT } from "../../../services/jwtService";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import { Toast } from "primereact/toast";
import { Button } from "primereact/button";
import { FileUpload } from "primereact/fileupload";
import { Rating } from "primereact/rating";
import { Toolbar } from "primereact/toolbar";
import { InputTextarea } from "primereact/inputtextarea";
import { RadioButton } from "primereact/radiobutton";
import { InputNumber } from "primereact/inputnumber";
import { Dropdown } from "primereact/dropdown";
import { Calendar } from "primereact/calendar";
import { Dialog } from "primereact/dialog";
import { InputText } from "primereact/inputtext";
import TextField from "../../../components/inputs/TextFieldGroup/TextFieldGroup";
import {
  getGroupsAsync,
  createGroup,
  deleteGroup,
  deleteSelectedGroups,
  updateGroupAsync,
} from "../../../services/groupService";
import { InputMask } from "primereact/inputmask";
import "primereact/resources/themes/saga-blue/theme.css";
import "primereact/resources/primereact.min.css";
import "primeicons/primeicons.css";
import { TabView, TabPanel } from "primereact/tabview";
import TeacherCreateGroupDataTable from "../../../components/table/TeacherCreateGroupDataTable";
import StudentsCreateGroupDataTable from "../../../components/table/StudentsCreateGroupDataTable";
import menuImage from "../../../images/menuBack.jpg";
import "react-pro-sidebar/dist/css/styles.css";
import { getStudentsByGroupIdAsync } from "../../../services/studentsService";
import {
  getTeacherByGroup,
  getTeachersAsync,
  DeleteTeacherAsync,
  CreateTeacher,
  UpdateTeacher,
  GetTeacherById,
} from "../../../services/teacherService";
import "./Teacher.css";
import classNames from "classnames";

const TeacherPage = () => {
  let emptyTeacher = {
    firstName: "",
    lastName: "",
    middleName: "",
    email: "",
    dayOfbirthday: "",
    phoneNumber: "",
  };
  const history = useHistory();
  const [teachers, setTeachers] = useState([]);
  const [selectedGroups, setSelectedGroups] = useState(null);
  const [inProgress, setInProgress] = useState(true);
  const [globalFilter, setGlobalFilter] = useState(null);
  const toast = useRef(null);
  const [submitted, setSubmitted] = useState(false);
  const [deleteGroupDialog, setDeleteGroupDialog] = useState(false);
  const [deleteGroupsDialog, setDeleteGroupsDialog] = useState(false);
  const [teacher, setTeacher] = useState(emptyTeacher);
  const [createDialog, setCreateDialog] = useState(false);
  const [selectedTeacher, setSelectedTeacher] = useState();
  const [errors, setErrors] = useState({});
  const [editDialog, setGroupEditDialog] = useState(false);
  const [studentsDialog, setStudentsDialog] = useState(false);
  const [selectedStudents, setSelectedStudents] = useState();
  const [date, setDate] = useState();

  const dt = useRef(null);

  const onInputChange = (e, name) => {
    const val = (e.target && e.target.value) || "";
    let _group = { ...teacher };
    _group[`${name}`] = val;

    setTeacher(_group);
  };

  const { t } = useTranslation();

  useEffect(() => {
    getTeacher();
  }, []);

  const getTeacher = async () => {
    const response = await getTeachersAsync();
    console.log("response=>", response);
    setTeachers(response.data);
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

  const header = (
    <div className="table-header">
      <span className="p-input-icon-left">
        <i className="pi pi-search" />
        <InputText
          type="search"
          onInput={(e) => setGlobalFilter(e.target.value)}
          placeholder="Search..."
        />
      </span>
    </div>
  );

  const openNew = () => {
    setTeacher(emptyTeacher);
    setSubmitted(false);
    setCreateDialog(true);
  };

  const leftToolbarTemplate = <React.Fragment></React.Fragment>;

  const rightToolbarTemplate = (
    <React.Fragment>
      <Button
        icon="pi pi-trash"
        className="p-button-danger"
        onClick={() => confirmDeleteSelected()}
        disabled={!selectedGroups || !selectedGroups.length}
      />
    </React.Fragment>
  );
  const confirmDeleteGroup = (group) => {
    setTeacher(group);
    setDeleteGroupDialog(true);
  };

  const actionBodyTemplate = (rowData) => {
    return (
      <React.Fragment>
        <Button
          style={{ marginLeft: "0.5vw" }}
          icon="pi pi-trash"
          className="p-button-rounded p-button-warning"
          onClick={() => confirmDeleteGroup(rowData)}
        />
      </React.Fragment>
    );
  };
  const updateGroup = async () => {
    setSubmitted(true);

    if (teacher.firstName.trim()) {
      let model = {
        id: teacher.id,
        firstName: teacher.firstName,
        lastName: teacher.lastName,
        middleName: teacher.middleName,
        email: teacher.email,
        DayOfbirthday: teacher.dayOfbirthday,
        phoneNumber: teacher.phoneNumber,
      };
      await UpdateTeacher(model)
        .then(
          async (response) => {
            setGroupEditDialog(false);

            console.log(response);
            setCreateDialog(false);
            getTeacher();
            toast.current.show({
              severity: "success",
              summary: "Successful",
              detail: "Data updated",
              life: 3000,
            });
            setTeacher(emptyTeacher);
            setSelectedTeacher(null);
            setSelectedStudents(null);
          },
          (err) => {
            //setErrors(err.response.data);
            console.log("Error:", err.response.data);
          }
        )
        .catch((err) => {
          console.log("Global Server problen in controler message", err);
        });
    }
  };
  const saveTeacher = async () => {
    setSubmitted(true);

    if (teacher.firstName.trim()) {
      let model = {
        firstName: teacher.firstName,
        lastName: teacher.lastName,
        middleName: teacher.middleName,
        email: teacher.email,
        dayOfbirthday: teacher.dayOfbirthday,
        phoneNumber: teacher.phoneNumber,
      };

      await CreateTeacher(model)
        .then(
          async (response) => {
            console.log(response);
            setCreateDialog(false);
            getTeacher();
            toast.current.show({
              severity: "success",
              summary: "Successful",
              detail: "Teacher Created",
              life: 3000,
            });
            setTeacher(emptyTeacher);
            setSelectedTeacher(null);
            setSelectedStudents(null);
          },
          (err) => {
            //setErrors(err.response.data);
            console.log("Error:", err.response.data);
          }
        )
        .catch((err) => {
          console.log("Global Server problen in controler message", err);
        });
    }
  };

  const groupsDialogFooter = (
    <React.Fragment>
      <Button
        label={t("Common.Cancel")}
        icon="pi pi-times"
        className="p-button-text"
        onClick={() => hideDialog()}
      />
      <Button
        label={t("Common.Save")}
        icon="pi pi-check"
        className="p-button-text"
        onClick={saveTeacher}
      />
    </React.Fragment>
  );

  const updateGroupsDialogFooter = (
    <React.Fragment>
      <Button
        label={t("Common.Cancel")}
        icon="pi pi-times"
        className="p-button-text"
        onClick={() => {
          setGroupEditDialog(false);
        }}
      />
      <Button
        label={t("Common.Save")}
        icon="pi pi-check"
        className="p-button-text"
        onClick={() => updateGroup()}
      />
    </React.Fragment>
  );

  const selectStudentDialogFooter = (
    <React.Fragment>
      <Button
        label={t("Common.Cancel")}
        icon="pi pi-times"
        className="p-button-text"
        onClick={() => {
          setSelectedStudents(null);
          setStudentsDialog(false);
        }}
      />
      <Button
        label={t("Common.Save")}
        icon="pi pi-check"
        className="p-button-text"
        onClick={() => {
          setStudentsDialog(false);
        }}
      />
    </React.Fragment>
  );

  const deleteSelected = async () => {
    try {
      let selectedId = [];
      selectedGroups.forEach((element) => {
        selectedId.push(element.id);
      });
      await DeleteTeacherAsync(selectedId)
        .then(
          async (response) => {
            console.log(response);
            getTeacher();

            setDeleteGroupsDialog(false);
            setSelectedGroups(null);
            toast.current.show({
              severity: "success",
              summary: "Successful",
              detail: "Products Deleted",
              life: 3000,
            });

            setTeacher(emptyTeacher);
          },
          (err) => {
            //setErrors(err.response.data);
            console.log("Error:", err.response);
          }
        )
        .catch((err) => {
          console.log("Global Server problen in controler message", err);
        });
    } catch {}
  };

  const editTeacher = async (teacher) => {
    console.log("Teacher=>", teacher);
    const response = await GetTeacherById(teacher.id);
    setTeacher({ ...response.data });
    console.log("group=>", teacher);
    setGroupEditDialog(true);
  };

  const deleteGroupsDialogFooter = (
    <React.Fragment>
      <Button
        label="No"
        icon="pi pi-times"
        className="p-button-text"
        onClick={() => hideDeleteGroupsDialog()}
      />
      <Button
        label="Yes"
        icon="pi pi-check"
        className="p-button-text"
        onClick={deleteSelected}
      />
    </React.Fragment>
  );
  const hideDeleteGroupsDialog = () => {
    setDeleteGroupsDialog(false);
  };
  const confirmDeleteSelected = () => {
    console.log("confirmDeleteSelected");
    setDeleteGroupsDialog(true);
  };
  const deleteGroupHandler = async () => {
    await DeleteTeacherAsync([teacher.id])
      .then(
        async (response) => {
          console.log(response);
          setDeleteGroupDialog(false);
          getTeacher();

          toast.current.show({
            severity: "success",
            summary: "Successful",
            detail: "Group Deleted",
            life: 3000,
          });

          setTeacher(emptyTeacher);
        },
        (err) => {
          //setErrors(err.response.data);
          console.log("Error:", err.response);
        }
      )
      .catch((err) => {
        console.log("Global Server problen in controler message", err);
      });
  };
  const deleteGroupDialogFooter = (
    <React.Fragment>
      <Button
        label="No"
        icon="pi pi-times"
        className="p-button-text"
        onClick={() => hideDeleteGroupDialog()}
      />
      <Button
        label="Yes"
        icon="pi pi-check"
        className="p-button-text"
        onClick={deleteGroupHandler}
      />
    </React.Fragment>
  );

  const hideDeleteGroupDialog = () => {
    setDeleteGroupDialog(false);
  };
  const hideDialog = () => {
    setSubmitted(false);
    setCreateDialog(false);
  };

  const getTeachersWitoutGroup = async () => {
    return;
  };

  const imageBodyTemplate = (rowData) => {
    return (
      <img
        src={`${rowData.image}`}
        onError={(e) =>
          (e.target.src =
            "https://www.primefaces.org/wp-content/uploads/2020/05/placeholder.png")
        }
        alt={rowData.image}
        className="teacher-image"
      />
    );
  };

  const dayOfbirthdayTemplate = (rowData) => {
    let date = new Date(rowData.dayOfbirthday).toDateString();
    return <span>{date} </span>;
  };
  return (
    <>
      {inProgress ? (
        <div></div>
      ) : (
        <>
          {" "}
          <div className="main-page datatable-crud-demo">
            <div className="container card">
              <Toast ref={toast} />
              <Toolbar
                className="p-mb-4"
                left={leftToolbarTemplate}
                right={rightToolbarTemplate}
              ></Toolbar>
              <DataTable
                ref={dt}
                value={teachers}
                selection={selectedGroups}
                onSelectionChange={(e) => setSelectedGroups(e.value)}
                dataKey="id"
                paginator
                rows={10}
                rowsPerPageOptions={[5, 10, 25]}
                paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
                header={header}
                className="p-component"
                globalFilter={globalFilter}
                scrollable
                scrollHeight="60vh"
              >
                <Column
                  selectionMode="multiple"
                  headerStyle={{ width: "3rem" }}
                ></Column>
                <Column
                  field="id"
                  header="id"
                  sortable
                  style={{ display: "none" }}
                ></Column>
                <Column
                  field="name"
                  header={t("Common.Name")}
                  sortable
                ></Column>
                <Column
                  field="nameGroup"
                  header={t("Common.Group")}
                  sortable
                ></Column>
                <Column header="Image" body={imageBodyTemplate}></Column>
                <Column field="email" header="Email" sortable></Column>
                <Column
                  field="phoneNumber"
                  header={t("Common.PhoneNumber")}
                  sortable
                ></Column>
                <Column
                  field="dayOfbirthday"
                  header={t("Common.DayOfbirthdayTemplate")}
                  body={dayOfbirthdayTemplate}
                  sortable
                ></Column>
                <Column body={actionBodyTemplate}></Column>
              </DataTable>
              <Dialog
                visible={deleteGroupDialog}
                style={{ width: "450px" }}
                header="Confirm"
                modal
                footer={deleteGroupDialogFooter}
                onHide={hideDeleteGroupDialog}
              >
                <div className="confirmation-content">
                  <i
                    className="pi pi-exclamation-triangle p-mr-3"
                    style={{ fontSize: "2rem" }}
                  />
                  {teacher && (
                    <span>
                      Are you sure you want to delete <b>{teacher.name}</b>?
                    </span>
                  )}
                </div>
              </Dialog>
              <Dialog
                visible={createDialog}
                style={{ width: "80vw", height: "90vh" }}
                header="Teacher Details"
                modal
                className="p-fluid"
                footer={groupsDialogFooter}
                onHide={hideDialog}
              >
                <div className="p-field">
                  {" "}
                  <br />
                  <br /> <label htmlFor="firstName">First name</label>
                  <InputText
                    id="firstName"
                    value={teacher.firstName}
                    onChange={(e) => onInputChange(e, "firstName")}
                    required
                    autoFocus
                    className={classNames({
                      "p-invalid": submitted && !teacher.firstName,
                    })}
                  />
                  {submitted && !teacher.firstName && (
                    <small className="p-error">First name is required.</small>
                  )}{" "}
                  <br />
                  <br /> <label htmlFor="middleName">Middle name</label>
                  <InputText
                    id="middleName"
                    value={teacher.middleName}
                    onChange={(e) => onInputChange(e, "middleName")}
                    required
                    className={classNames({
                      "p-invalid": submitted && !teacher.middleName,
                    })}
                  />
                  {submitted && !teacher.middleName && (
                    <small className="p-error">Middle name is required.</small>
                  )}{" "}
                  <br />
                  <br /> <label htmlFor="lastName">Last name</label>
                  <InputText
                    id="lastName"
                    value={teacher.lastName}
                    onChange={(e) => onInputChange(e, "lastName")}
                    required
                    className={classNames({
                      "p-invalid": submitted && !teacher.lastName,
                    })}
                  />
                  {submitted && !teacher.lastName && (
                    <small className="p-error">Last name is required.</small>
                  )}
                  <br />
                  <br />
                  <label htmlFor="name">Email</label>
                  <InputText
                    id="email"
                    value={teacher.email}
                    onChange={(e) => onInputChange(e, "email")}
                    required
                    className={classNames({
                      "p-invalid": submitted && !teacher.email,
                    })}
                  />
                  {submitted && !teacher.email && (
                    <small className="p-error">Email is required.</small>
                  )}
                  <br />
                  <br /> <label htmlFor="dayOfbirthday">Date</label>
                  <input
                    id="dayOfbirthday"
                    value={teacher.dayOfbirthday}
                    onChange={(e) => onInputChange(e, "dayOfbirthday")}
                    type="date"
                    className={"p-inputtext p-component p-filled"}
                  />
                  {submitted && !teacher.dayOfbirthday && (
                    <small className="p-error">Date is required.</small>
                  )}
                  <br />
                  <br /> <label htmlFor="phoneNumber">Phone</label>
                  <InputMask
                    id="phoneNumber"
                    mask="999 999 9999"
                    value={teacher.phoneNumber}
                    onChange={(e) => onInputChange(e, "phoneNumber")}
                    className={classNames({
                      "p-invalid": submitted && !teacher.phoneNumber,
                    })}
                  ></InputMask>
                  {submitted && !teacher.phoneNumber && (
                    <small className="p-error">Phone is required.</small>
                  )}
                </div>
              </Dialog>
              <Dialog
                visible={studentsDialog}
                style={{ width: "80vw", height: "90vh" }}
                header="Students Details"
                modal
                className="p-fluid"
                footer={selectStudentDialogFooter}
                onHide={() => {
                  setStudentsDialog(false);
                }}
              >
                <div className="p-field">
                  <StudentsCreateGroupDataTable
                    selection={selectedStudents}
                    idEdit={editDialog}
                    setSelected={setSelectedStudents}
                  />
                </div>
              </Dialog>
              <Dialog
                visible={editDialog}
                style={{ width: "80vw", height: "90vh" }}
                header="Edit groups"
                modal
                className="p-fluid"
                footer={updateGroupsDialogFooter}
                onHide={() => {
                  setGroupEditDialog(false);
                }}
              >
                <div className="p-field">
                  {" "}
                  <br />
                  <br /> <label htmlFor="firstName">First name</label>
                  <InputText
                    id="firstName"
                    value={teacher.firstName}
                    onChange={(e) => onInputChange(e, "firstName")}
                    required
                    autoFocus
                    className={classNames({
                      "p-invalid": submitted && !teacher.firstName,
                    })}
                  />
                  {submitted && !teacher.firstName && (
                    <small className="p-error">First name is required.</small>
                  )}{" "}
                  <br />
                  <br /> <label htmlFor="middleName">Middle name</label>
                  <InputText
                    id="middleName"
                    value={teacher.middleName}
                    onChange={(e) => onInputChange(e, "middleName")}
                    required
                    className={classNames({
                      "p-invalid": submitted && !teacher.middleName,
                    })}
                  />
                  {submitted && !teacher.middleName && (
                    <small className="p-error">Middle name is required.</small>
                  )}{" "}
                  <br />
                  <br /> <label htmlFor="lastName">Last name</label>
                  <InputText
                    id="lastName"
                    value={teacher.lastName}
                    onChange={(e) => onInputChange(e, "lastName")}
                    required
                    className={classNames({
                      "p-invalid": submitted && !teacher.lastName,
                    })}
                  />
                  {submitted && !teacher.lastName && (
                    <small className="p-error">Last name is required.</small>
                  )}
                  <br />
                  <br />
                  <label htmlFor="name">Email</label>
                  <InputText
                    id="email"
                    value={teacher.email}
                    onChange={(e) => onInputChange(e, "email")}
                    required
                    className={classNames({
                      "p-invalid": submitted && !teacher.email,
                    })}
                  />
                  {submitted && !teacher.email && (
                    <small className="p-error">Email is required.</small>
                  )}
                  <br />
                  <br /> <label htmlFor="dayOfbirthday">Date</label>
                  <input
                    id="dayOfbirthday"
                    value={teacher.dayOfbirthday}
                    onChange={(e) => onInputChange(e, "dayOfbirthday")}
                    type="date"
                    required
                    className={"p-inputtext p-component p-filled"}
                  />
                  {submitted && !teacher.dayOfbirthday && (
                    <small className="p-error">Date is required.</small>
                  )}
                  <br />
                  <br /> <label htmlFor="phoneNumber">Phone</label>
                  <InputMask
                    id="phoneNumber"
                    mask="999 999 9999"
                    value={teacher.phoneNumber}
                    onChange={(e) => onInputChange(e, "phoneNumber")}
                    className={classNames({
                      "p-invalid": submitted && !teacher.phoneNumber,
                    })}
                  ></InputMask>
                  {submitted && !teacher.phoneNumber && (
                    <small className="p-error">Phone is required.</small>
                  )}
                </div>
              </Dialog>

              <Dialog
                visible={deleteGroupsDialog}
                style={{ width: "35vw" }}
                header="Confirm"
                modal
                footer={deleteGroupsDialogFooter}
                onHide={hideDeleteGroupsDialog}
              >
                <div className="confirmation-content">
                  <i
                    className="pi pi-exclamation-triangle p-mr-3"
                    style={{ fontSize: "2rem" }}
                  />
                  {teacher && (
                    <span>
                      Are you sure you want to delete the selected groups?
                    </span>
                  )}
                </div>
              </Dialog>
            </div>
          </div>
        </>
      )}
    </>
  );
};

export default TeacherPage;
