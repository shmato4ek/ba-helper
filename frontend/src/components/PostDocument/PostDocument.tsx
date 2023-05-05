import { Formik, FormikProps } from "formik";
import React, { FC } from "react";
import styled from "styled-components";
import {
  EditPostProjectDto,
  getInitPutUserStory,
  Glossary,
  PostDocumentDto,
  PutGlossary,
  PutUserStory,
  UserStory,
} from "../../store/types";
import Button from "../Button/Button";
import Icon from "../Icon/Icon";
import {
  AlignCenter,
  VerticalGrid,
  VerticalMargins,
  Wrapper,
} from "../Utils/Utils";
import FormDatepicker from "../Form/FormDatepicker/FormDatepicker";
import FormStringField from "../Form/FormStringField/FormStringField";
import FormTextareaField from "../Form/FormTextareaField/FormTextareaField";
import FormError from "../Form/FormError/FormError";
import PostGlossaryContainer from "../../containers/PostGlossaryContainer/PostGlossaryContainer";
import PostUserStoryContainer from "../../containers/PostUserStoryContainer/PostUserStoryContainer";

export const Header = styled.h1``;

export const HorizontalGrid = styled.div`
  display: grid;
  grid-gap: 20px;
  grid-auto-flow: column;

  grid-template-columns: 1fr 1fr;

  align-items: center;
`;

export const FieldGrid = styled.div`
  display: grid;
  grid-auto-flow: column;
  grid-auto-columns: max-content;
  align-items: center;

  grid-gap: 10px;
`;

export const Table = styled.table`
  width: 100%;
  font-family: Arial, Helvetica, sans-serif;
  border-collapse: collapse;

  overflow: scroll;
`;

export const TH = styled.th`
  text-align: left;

  padding: 12px;
`;

export const TD = styled.td`
  padding: 12px;

  background-color: #c8efcc;
  border-bottom: 1px solid black;
`;

export const TR = styled.tr`
  height: 10px;
`;

const Footer = styled.footer`
  display: grid;
  grid-template-columns: auto 1fr auto;
  align-items: center;
  font-size: 32px;
`;

const GridWrapper = styled.div`
  display: grid;
  grid-gap: 20px;
  grid-template-rows: 1fr auto;
`;

type Props = {
  postDocument: PostDocumentDto;

  onValidate: (values: PostDocumentDto) => any;
  onSubmit: (values: PostDocumentDto) => void;

  glossaries: PutGlossary[];
  userStories: PutUserStory[];
  setGlossaries: React.Dispatch<React.SetStateAction<PutGlossary[]>>;
  setUserStories: React.Dispatch<React.SetStateAction<PutUserStory[]>>;

  isElemsError: boolean
  setIsElemsError: React.Dispatch<React.SetStateAction<boolean>>;
};

const PostDocument: FC<Props> = (params) => {
  return (
    <GridWrapper>
      <Formik
        initialValues={params.postDocument}
        enableReinitialize={true}
        validate={params.onValidate}
        onSubmit={params.onSubmit}
      >
        {({ handleSubmit }: FormikProps<PostDocumentDto>) => (
          <>
            <Wrapper>
              <VerticalMargins>
                <VerticalGrid>
                  <HorizontalGrid>
                    <FieldGrid>
                      <Header>Назва документу:</Header>
                      <FormStringField
                        placeholder="Документ 1"
                        name={"name"}
                        label=""
                      />
                      <FormError name="name" />
                    </FieldGrid>
                  </HorizontalGrid>
                  <Header>Ціль проекту:</Header>
                  <FormTextareaField
                    placeholder="Ціль проекту"
                    name={"projectAim"}
                    label=""
                    textAreaStyle={{ width: "100%" }}
                  />
                  <FormError name="projectAim" />
                  <Header>Глосарій:</Header>
                      <HorizontalGrid>
                        <VerticalGrid>
                          <Header>Термін:</Header>
                        </VerticalGrid>
                        <VerticalGrid>
                          <Header>Визначення:</Header>
                        </VerticalGrid>
                      </HorizontalGrid>
                  {params.glossaries.map((glossary, index) => {
                    return (
                      <PostGlossaryContainer
                        glossary={glossary}
                        setGlossaries={params.setGlossaries}
                        setIsElemsError={params.setIsElemsError}
                        index={index}
                      />
                    );
                  })}
                  <AlignCenter>
                    <Button
                      buttonType="button"
                      styleType="simple"
                      onClick={() =>
                        params.setGlossaries([
                          ...params.glossaries,
                          {
                            term: "",
                            definition: "",
                          },
                        ])
                      }
                    >
                      Додати термін
                    </Button>
                  </AlignCenter>
                  <Header>Користувацькі історії:</Header>
                  <b>Створені користувацькі історії</b>
                  {params.userStories.map((userStory, index) => {
                    return <PostUserStoryContainer 
                      userStoryIndex={index}
                      setIsError={params.setIsElemsError}
                      setUserStories={params.setUserStories}
                      userStories={params.userStories}
                      userStory={userStory}
                    />
                  })}
                  <Footer>
                    <Button
                        buttonType="button"
                        styleType="simple"
                        onClick={() =>
                          params.setUserStories([
                            ...params.userStories,
                            getInitPutUserStory(),
                          ])
                        }
                      >
                      Додати Юзер Сторі
                    </Button>
                    <div></div>
                    <Button
                      buttonType="submit"
                      styleType="simple"
                      onClick={() => {
                        handleSubmit();
                      }}
                    >
                      Створити
                    </Button>
                  </Footer>
                </VerticalGrid>
              </VerticalMargins>
            </Wrapper>
          </>
        )}
      </Formik>
    </GridWrapper>
  );
};

export default PostDocument;
