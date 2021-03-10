const validateLoginFields = (items) => {
  const regex_email = /^[a-zA-Z0-9.]+@[a-zA-Z0-9]+\.[A-Za-z]+$/;
  let errors = {};
  const { email, password } = items;
  if (email.trim() === "") errors.email = "Поле не може бути пустим!";
  else if (!regex_email.test(email.trim()))
    errors.email = "Не правильний формат електронної пошти!";

  if (password.trim() === "") errors.password = "Поле не може бути пустим!";
  return errors;
};

export { validateLoginFields };
