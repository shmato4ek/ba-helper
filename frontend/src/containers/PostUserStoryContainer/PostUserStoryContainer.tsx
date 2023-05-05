import React, { FC, useCallback, useState } from "react";

import * as yup from "yup";
import * as _ from "lodash";
import { HorizontalGrid } from "../../components/PostDocument/PostDocument";
import FormTextareaField from "../../components/Form/FormTextareaField/FormTextareaField";
import FormError from "../../components/Form/FormError/FormError";
import { Formik, FormikProps } from "formik";
import { validateStraight } from "../../yup";
import {
  CreateErrorObject,
  getInitUSAcceptanceCriteria,
  getInitUSFormula,
  PutGlossary,
  PutUserStory,
} from "../../store/types";
import styled from "styled-components";
import FormStringField from "../../components/Form/FormStringField/FormStringField";
import Button from "../../components/Button/Button";
import PostUSAcceptanceCriteria from "../PostUSAcceptanceCriteria/PostUSAcceptanceCriteria";
import PostUSFormula from "../PostUSFormula/PostUSFormula";

type Props = {
  userStoryIndex: number;
  userStory: PutUserStory;
  setUserStories: React.Dispatch<React.SetStateAction<PutUserStory[]>>;
  userStories: PutUserStory[];

  setIsError: React.Dispatch<React.SetStateAction<boolean>>;
};

export const Card = styled.div`
  background-color: #c8efcc;
  border-radius: 8px;
  padding: 20px;
`;

const Footer = styled.footer`
  display: grid;
  grid-template-columns: auto 1fr auto;
  align-items: center;
  font-size: 32px;
`;

const VerticalMargins = styled.div`
  margin: 20px 0px;
`;

const PostUserStoryContainer: FC<Props> = ({
  userStoryIndex,
  userStory,
  setUserStories,
  userStories,
  setIsError,
}) => {
  const [isElemsError, setIsElemsError] = useState<boolean>(false);

  const onValidate = useCallback(
    (values: PutUserStory) => {
      // console.log("Post Glossary values validate");
      // console.log(JSON.stringify(values, null, 2));

      let formErrors: CreateErrorObject<PutUserStory> = {};

      formErrors.name = validateStraight(
        yup
          .string()
          .max(255, "Ім'я повинно бути не більше 255 символів")
          .required("Обов'язково"),
        values.name
      );

      formErrors = _.pickBy(formErrors, _.identity);

      setUserStories((userStory) => {
        const newUserStories = userStory.map((x, ind) => {
          if (ind === userStoryIndex) {
            return values;
          }

          return x;
        });

        return newUserStories;
      });

      if (formErrors.name || isElemsError) {
        setIsError(true);
      } else {
        setIsError(false);
      }

      return formErrors;
    },
    [userStoryIndex, setIsError, isElemsError, setUserStories]
  );

  return (
    <Formik
      initialValues={userStory}
      enableReinitialize={true}
      validate={onValidate}
      onSubmit={() => {}}
    >
      <Card>
        <HorizontalGrid>
          <FormStringField placeholder="" name={"name"} label="" />
          <FormError name="name" />
        </HorizontalGrid>
        {userStory.acceptanceCriterias.map((acceptanceCriteria, index) => {
          return <VerticalMargins>
            <PostUSAcceptanceCriteria
              acceptanceCriteriaIndex={index}
              userStoryIndex={userStoryIndex}
              userStories={userStories}
              setIsError={setIsElemsError}
              setUserStories={setUserStories}
            />
          </VerticalMargins>
        })}
        {userStory.userStoryFormulas.map((usFormula, index) => {
          return <VerticalMargins>
            <PostUSFormula
              formulaIndex={index}
              userStoryIndex={userStoryIndex}
              userStories={userStories}
              setIsError={setIsElemsError}
              setUserStories={setUserStories}
            />
          </VerticalMargins>
        })}
        <Footer>
          <Button
            buttonType="button"
            styleType="simple"
            onClick={() =>
              setUserStories(
                userStories.map((x, ind) => {
                  if (ind === userStoryIndex) {
                    x.acceptanceCriterias = [
                      ...x.acceptanceCriterias,
                      getInitUSAcceptanceCriteria(),
                    ];
                  }

                  return x;
                })
              )
            }
          >
            Додати Acceptance Criteria
          </Button>
          <div></div>
          <Button
            buttonType="button"
            styleType="simple"
            onClick={() =>
              setUserStories(
                userStories.map((x, ind) => {
                  if (ind === userStoryIndex) {
                    x.userStoryFormulas = [
                      ...x.userStoryFormulas,
                      getInitUSFormula(),
                    ];
                  }

                  return x;
                })
              )
            }
          >
            Додати User Formula
          </Button>
        </Footer>
      </Card>
    </Formik>
  );
};

export default PostUserStoryContainer;
