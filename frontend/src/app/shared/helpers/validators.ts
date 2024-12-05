// check if the value has a minimum length
export const checkLength = (value: string, length: number) =>
  value.length >= length;

// check if there is at least one lettere uppercase
export const checkUppercase = (value: string) => /[A-Z]/.test(value);

// check if there is at least one lettere lowercase
export const checkLowercase = (value: string) => /[a-z]/.test(value);

// check if there is at least one number
export const checkNumber = (value: string) => /\d/.test(value);

// check if there is at least one special character
export const checkSpecial = (value: string) =>
  /[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]+/.test(value);

// check if the email is valid
export const checkEmail = (value: string) =>
  /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/.test(value);
