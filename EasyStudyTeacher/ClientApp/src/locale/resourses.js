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
        logOut: "Вийти",
        Search: "Пошук...",
        Created: "Створено",
        Name: "Ім'я",
        required: "обов'язкове поле",
        AddStudent: "Добавити студентів",
        Teacher: "Вчитель",
        StudentsDetails: "Інформація про студента",
        EditGroups: "Редагувати групу",
        Title: "Назва",
        DeleteGroupConfirm: "Ви впевнені, що хочете видалити вибрані групи?",
        Cancel: "Відміна",
        Save: "Зберегти",
        QuantityOfStudents: "Кількість студентів",
        PhoneNumber: "Номер телефону",
        DayOfbirthdayTemplate: "Дата народження",
        Group: "Група",
      },
    },
  },
  en: {
    translation: {
      Admin: {
        GroupManagement: "Group management",
        GroupTeacher: "Teacher management",
        GroupStudent: "Student management",
      },
      Common: {
        ShowGroup: "View groups",
        CreateGroup: "Create a group",
        logOut: "Log out",
        Search: "Search...",
        Created: "Created",
        Name: "Name",
        required: "is required",
        AddStudent: "Add students",
        Teacher: "Teacher",
        StudentsDetails: "Information about students",
        EditGroups: "Edit group",
        Title: "Title",
        DeleteGroupConfirm:
          "Are you sure you want to delete the selected groups?",
        Cancel: "Cancel",
        Save: "Save",
        QuantityOfStudents: "Quantity of students",
        PhoneNumber: "Phone number",
        DayOfbirthdayTemplate: "Day of birthday",
        Group: "Group",
      },
    },
  },
};

i18n
  .use(initReactI18next) // passes i18n down to react-i18next
  .init({
    resources,
    lng: "en",
    interpolation: {
      escapeValue: false, // react already safes from xss
    },
  });

export default i18n;
