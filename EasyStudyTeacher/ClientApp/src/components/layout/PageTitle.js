import React from "react";
import { useTranslation } from "react-i18next";

import styles from "./PageTitle.module.css";

/**
 * Page title component
 */
const PageTitle = React.memo(({ Icon, title, subtitle }) => {
  const { t } = useTranslation();

  return (
    <>
      <div className={styles["page-title"]} style={{ color: accentColor }}>
        <div>{Icon ? <Icon className={styles["icon"]} /> : ""} </div>
        {t(`${title}`)}
      </div>
      <hr style={{ width: "90vw" }}></hr>
    </>
  );
});

export default PageTitle;
