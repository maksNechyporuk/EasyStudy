import React, { useState, useEffect, useRef } from "react";
import { useTranslation } from "react-i18next";
import { getRegisterPageRoute } from "../../routes/routes";
import { useHistory } from "react-router";
import { loginByJWT } from "../../services/jwtService";
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
import { Dialog } from "primereact/dialog";
import { InputText } from "primereact/inputtext";
import { Table, Radio, Divider } from "antd";
import { getStudentsWithoutGroupAsync } from "../../services/studentsService";
import "primereact/resources/themes/saga-blue/theme.css";
import "primereact/resources/primereact.min.css";
import "primeicons/primeicons.css";
import "react-pro-sidebar/dist/css/styles.css";
import "./TeacherCreateGroupDataTable.css";

const StudentsCreateGroupDataTable = ({ setSelected, selection, idEdit }) => {
  let emptyGroup = {
    id: null,
    name: "",
    teacher: "",
    teacherId: "",
  };
  const [students, setStudents] = useState([]);
  const [inProgress, setInProgress] = useState(true);
  const [globalFilter, setGlobalFilter] = useState(null);
  const toast = useRef(null);
  const dt = useRef(null);
  const { t } = useTranslation();

  useEffect(() => {
    getStudents();
  }, []);

  useEffect(() => {
    console.log("students=>", students);
  }, [students]);

  const getStudents = async () => {
    const response = await getStudentsWithoutGroupAsync();
    var a = [];
    console.log("selection?.length=>", selection?.length);
    console.log("selection=>", selection);
    if (idEdit && selection?.length > 0) {
      setStudents([...selection, ...response.data]);
    } else {
      setStudents(response.data);
    }
    setInProgress(false);
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
        className="product-image"
      />
    );
  };

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

  return (
    <>
      {inProgress ? (
        <div></div>
      ) : (
        <>
          <DataTable
            ref={dt}
            value={students}
            onSelectionChange={(e) => {
              setSelected(e.value);
            }}
            selection={selection}
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
              field="id"
              header="id"
              sortable
              style={{ display: "none" }}
            ></Column>
            <Column
              selectionMode="multiple"
              headerStyle={{ width: "3rem" }}
            ></Column>
            <Column field="name" header="Ім'я" sortable></Column>
            <Column field="email" header="Email" sortable></Column>
            {/* <Column header="Image" body={imageBodyTemplate}></Column> */}
          </DataTable>
        </>
      )}
    </>
  );
};

export default StudentsCreateGroupDataTable;
