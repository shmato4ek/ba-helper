import React, { FC, useCallback, useState } from "react";

import * as yup from "yup";
import * as _ from "lodash";
import { HorizontalGrid } from "../../components/PostDocument/PostDocument";
import { VerticalGrid } from "../../components/Utils/Utils";
import FormTextareaField from "../../components/Form/FormTextareaField/FormTextareaField";
import FormError from "../../components/Form/FormError/FormError";
import { Formik, FormikProps } from "formik";
import { validateStraight } from "../../yup";
import { CreateErrorObject, PutGlossary } from "../../store/types";

type Props = {
  index: number;
  glossary: PutGlossary;
  setGlossaries: React.Dispatch<React.SetStateAction<PutGlossary[]>>;

  setIsElemsError: React.Dispatch<React.SetStateAction<boolean>>;
};

const PostGlossaryContainer: FC<Props> = ({
  index,
  glossary,
  setGlossaries,
  setIsElemsError
}) => {
  const onValidate = useCallback((values: PutGlossary) => {
    // console.log("Post Glossary values validate");
    // console.log(JSON.stringify(values, null, 2));

    let formErrors: CreateErrorObject<PutGlossary> = {};

    formErrors.term = validateStraight(
      yup
        .string()
        .max(255, "Термін повинен бути не більше 255 символів")
        .required("Обов'язково"),
      values.term
    );
    formErrors.definition = validateStraight(
      yup
        .string()
        .max(255, "Опис повинен бути не більше 255 символів")
        .required("Обов'язково"),
      values.definition
    );

    formErrors = _.pickBy(formErrors, _.identity);

    setGlossaries((glossaries) => {

      const newGlossaries = glossaries.map((x, ind) => {
        if(ind === index) {
          return values;
        }

        return x;
      })
      
      return newGlossaries;
    });

    if(formErrors.term || formErrors.definition) {
      setIsElemsError(true)
    } else {
      setIsElemsError(false)
    }

    return formErrors;
  }, [index, setGlossaries, setIsElemsError]);

  return (
    <Formik
      initialValues={glossary}
      enableReinitialize={true}
      validate={onValidate}
      onSubmit={() => {}}
    >
      <HorizontalGrid>
        <VerticalGrid>
          <FormTextareaField
            placeholder="Термін"
            name={"term"}
            label=""
            textAreaStyle={{ width: "100%" }}
          />
          <FormError name="term" />
        </VerticalGrid>
        <VerticalGrid>
          <FormTextareaField
            placeholder="Визначення"
            name={"definition"}
            label=""
            textAreaStyle={{ width: "100%" }}
          />
          <FormError name="definition" />
        </VerticalGrid>
      </HorizontalGrid>
    </Formik>
  );
};

export default PostGlossaryContainer;
