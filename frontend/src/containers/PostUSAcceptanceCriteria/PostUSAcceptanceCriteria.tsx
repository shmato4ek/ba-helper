import React, { FC, useCallback, useState } from "react";

import * as yup from "yup";
import * as _ from "lodash";
import { HorizontalGrid } from "../../components/PostDocument/PostDocument";
import FormError from "../../components/Form/FormError/FormError";
import { Formik } from "formik";
import { validateStraight } from "../../yup";
import { CreateErrorObject, EditPutUSAcceptanceCriteria, PutUSAcceptanceCriteria, PutUserStory, UserStory, } from "../../store/types";
import styled from "styled-components";
import FormStringField from "../../components/Form/FormStringField/FormStringField";

type Props = {
  userStories: PutUserStory[];
  setUserStories: React.Dispatch<React.SetStateAction<PutUserStory[]>>;

  userStoryIndex: number;
  acceptanceCriteriaIndex: number;

  setIsError: React.Dispatch<React.SetStateAction<boolean>>;
};

export const Card = styled.div`
  background-color: #C8EFCC;
  border-radius: 8px;
  padding: 20px;
`;

const PostUSAcceptanceCriteria: FC<Props> = ({
  userStories,
  userStoryIndex,
  acceptanceCriteriaIndex,
  setUserStories,
  setIsError
}) => {
  const onValidate = useCallback((values: EditPutUSAcceptanceCriteria) => {
    // console.log("Post Glossary values validate");
    // console.log(JSON.stringify(values, null, 2));

    let formErrors: CreateErrorObject<EditPutUSAcceptanceCriteria> = {};

    formErrors.given = validateStraight(
      yup
        .string()
        .min(1, "Мінімму 1 символ")
        .required("Обов'язково"),
      values.given
    );
    formErrors.when = validateStraight(
      yup
        .string()
        .min(1, "Мінімму 1 символ")
        .required("Обов'язково"),
      values.when
    );
    formErrors.then = validateStraight(
      yup
        .string()
        .min(1, "Мінімму 1 символ")
        .required("Обов'язково"),
      values.then
    );

    formErrors = _.pickBy(formErrors, _.identity);

    const newUserStories = userStories.map((userStory, ind) => {
      if(ind === userStoryIndex) {
        userStory.acceptanceCriterias[acceptanceCriteriaIndex].text = `Given ${values.given} when ${values.when} then ${values.then}`;
      }

      return userStory;
    })
    setUserStories(newUserStories);

    if(formErrors.given || formErrors.when || formErrors.then) {
      setIsError(true)
    } else {
      setIsError(false)
    }

    return formErrors;
  }, [userStoryIndex, acceptanceCriteriaIndex, userStories, setIsError, setUserStories]);

  const editPutUSAcceptanceCriteria: EditPutUSAcceptanceCriteria = {
    given: '',
    when: '',
    then: '',
  }

  return (
    <Formik
      initialValues={editPutUSAcceptanceCriteria}
      enableReinitialize={true}
      validate={onValidate}
      onSubmit={() => {}}
    >
      <HorizontalGrid>
        <b>GIVEN</b>
        <FormStringField
          placeholder=""
          name={"given"}
          label=""
        />
        <FormError name="given" />
        <b>WHEN</b>
        <FormStringField
          placeholder=""
          name={"when"}
          label=""
        />
        <FormError name="when" />
        <b>THEN</b>
        <FormStringField
          placeholder=""
          name={"then"}
          label=""
        />
        <FormError name="then" />
      </HorizontalGrid>
    </Formik>
  );
};

export default PostUSAcceptanceCriteria;
