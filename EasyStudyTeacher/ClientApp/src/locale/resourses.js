import i18n from "i18next";
import { initReactI18next } from "react-i18next";

const resources = {
  uk: {
    translation: {
      Admin: {
        GroupManagement: "Керування групами",
        GroupTeacher: "Керування учителями",
        GroupStudent: "Керування студентами",
      },
      Common: {
        ShowGroup: "Перегляд груп",
        CreateGroup: "Створити групу",
      },
    },
  },
};

i18n
  .use(initReactI18next) // passes i18n down to react-i18next
  .init({
    resources,
    lng: "uk",
    interpolation: {
      escapeValue: false, // react already safes from xss
    },
  });

export default i18n;
