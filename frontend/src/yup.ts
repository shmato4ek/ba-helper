import { Schema } from "yup";

export const validateStraight = (baseSchema: Schema, value: any) => {
  try {
    baseSchema.validateSync(value);
  } catch (error: any) {
    return error.errors[0];
  }

  return undefined;
};
