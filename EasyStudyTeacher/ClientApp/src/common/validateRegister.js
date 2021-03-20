const validateRegisterFields = (items) => {
  const regex_phone = /^(?=\+?([0-9]{2})\s\(?([0-9]{3})\)\s?([0-9]{3})\s?([0-9]{2})\s?([0-9]{2})).{19}$/;
  const regex_password = /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[a-zA-Z]).{6,24}$/;
  const regex_email = /^[a-zA-Z0-9.]+@[a-zA-Z0-9]+\.[A-Za-z]+$/;
  let errors = {};
  const {
    email,
    lastName,
    firstName,
    middleName,
    password,
    school,
    region,
    photo,
  } = items;
  if (email.trim() === "") errors.email = "Поле не може бути пустим!";
  else if (!regex_email.test(email.trim()))
    errors.email = "Не правильний формат електронної пошти!";
  //if (email.trim() === "") errors.email = "Поле не може бути пустим!";
  if (lastName.trim() === "") errors.lastName = "Поле не може бути пустим!";
  if (school.trim() === "") errors.school = "Поле не може бути пустим!";
  if (region.trim() === "") errors.region = "Поле не може бути пустим!";
  if (firstName.trim() === "") errors.firstName = "Поле не може бути пустим!";
  if (middleName.trim() === "") errors.middleName = "Поле не може бути пустим!";
  if (password.trim() === "") errors.password = "Поле не може бути пустим!";
  else if (!regex_password.test(password.trim()))
    errors.password = "Цей пароль не є безпечним";

  return errors;
};

export { validateRegisterFields };
