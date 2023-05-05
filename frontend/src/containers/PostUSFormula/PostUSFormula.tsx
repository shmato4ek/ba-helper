import React, { FC, useCallback, useState } from "react";

import * as yup from "yup";
import * as _ from "lodash";
import { HorizontalGrid } from "../../components/PostDocument/PostDocument";
import FormError from "../../components/Form/FormError/FormError";
import { Formik } from "formik";
import { validateStraight } from "../../yup";
import { CreateErrorObject, EditPutUSAcceptanceCriteria, EditPutUSFormula, PutUSAcceptanceCriteria, PutUserStory, UserStory, } from "../../store/types";
import styled from "styled-components";
import FormStringField from "../../components/Form/FormStringField/FormStringField";

type Props = {
  userStories: PutUserStory[];
  setUserStories: React.Dispatch<React.SetStateAction<PutUserStory[]>>;

  userStoryIndex: number;
  formulaIndex: number;

  setIsError: React.Dispatch<React.SetStateAction<boolean>>;
};

export const Card = styled.div`
  background-color: #C8EFCC;
  border-radius: 8px;
  padding: 20px;
`;

const PostUSFormula: FC<Props> = ({
  userStories,
  userStoryIndex,
  formulaIndex,
  setUserStories,
  setIsError
}) => {
  const onValidate = useCallback((values: EditPutUSFormula) => {
    // console.log("Post Glossary values validate");
    // console.log(JSON.stringify(values, null, 2));

    let formErrors: CreateErrorObject<EditPutUSFormula> = {};

    formErrors.as = validateStraight(
      yup
        .string()
        .min(1, "Мінімму 1 символ")
        .required("Обов'язково"),
      values.as
    );
    formErrors.iWantTo = validateStraight(
      yup
        .string()
        .min(1, "Мінімму 1 символ")
        .required("Обов'язково"),
      values.iWantTo
    );
    formErrors.soThat = validateStraight(
      yup
        .string()
        .min(1, "Мінімму 1 символ")
        .required("Обов'язково"),
      values.soThat
    );

    formErrors = _.pickBy(formErrors, _.identity);

    const newUserStories = userStories.map((userStory, ind) => {
      if(ind === userStoryIndex) {
        userStory.userStoryFormulas[formulaIndex].text = `As ${values.as} I want to ${values.iWantTo} so that ${values.soThat}`;
      }

      return userStory;
    })
    setUserStories(newUserStories);

    if(formErrors.as || formErrors.iWantTo || formErrors.soThat) {
      setIsError(true)
    } else {
      setIsError(false)
    }

    return formErrors;
  }, [userStoryIndex, formulaIndex, userStories, setIsError, setUserStories]);

  const editPutUSAcceptanceCriteria: EditPutUSFormula = {
    as: '',
    iWantTo: '',
    soThat: '',
  }

  return (
    <Formik
      initialValues={editPutUSAcceptanceCriteria}
      enableReinitialize={true}
      validate={onValidate}
      onSubmit={() => {}}
    >
      <HorizontalGrid>
        <b>AS</b>
        <FormStringField
          placeholder=""
          name={"as"}
          label=""
        />
        <FormError name="as" />
        <b>I WANT TO</b>
        <FormStringField
          placeholder=""
          name={"iWantTo"}
          label=""
        />
        <FormError name="iWantTo" />
        <b>SO THAT</b>
        <FormStringField
          placeholder=""
          name={"soThat"}
          label=""
        />
        <FormError name="soThat" />
      </HorizontalGrid>
    </Formik>
  );
};

export default PostUSFormula;
