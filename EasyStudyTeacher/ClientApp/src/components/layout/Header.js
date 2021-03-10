import React, { useState } from "react";
import { useSelector } from "react-redux";
import { useTranslation } from "react-i18next";
import styles from "./Header.module.css";

/**
 * Page header component
 */
const Header = (props) => {
  const { t } = useTranslation();
  const [selectedLanguage, setSelectedLanguage] = useState("uk");
  return <div className={styles["header"]}></div>;
};

export default Header;
