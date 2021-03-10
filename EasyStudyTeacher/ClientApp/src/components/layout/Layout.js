import React from "react";
import styles from "./Layout.module.css";
import Header from "./Header";
import "../../styles.css";
/**
 * Page template component
 * @param {*} headerData object with page header data
 * @param {*} pageTitle - page title component
 * @param {*} footer - page footer component
 */
const Layout = ({ children }) => {
  let headerData = {
    languages: "uk",
  };
  return (
    <div className={styles["mainLayout"]}>
      <Header {...headerData} />
      {children}
    </div>
  );
};

export default Layout;
