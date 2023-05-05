import React, { useCallback, useState } from "react";
import { useDispatch, useSelector } from "react-redux";

import {
  PostDocument as PostDocumentAction,
  PostProject as PostProjectAction,
} from "../../../store/actions";
import {
  CreateErrorObject,
  getInitPutUserStory,
  PostDocumentDto,
  PutGlossary,
  PutUserStory,
} from "../../../store/types";
import * as yup from "yup";
import { validateStraight } from "../../../yup";
import * as _ from "lodash";
import { useNavigate } from "react-router";
import PostDocument from "../../../components/PostDocument/PostDocument";

const PostDocumentPage = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const [glossaries, setGlossaries] = useState<PutGlossary[]>([
    {
      term: "",
      definition: "",
    },
  ]);
  const [userStories, setUserStories] = useState<PutUserStory[]>([getInitPutUserStory()]);
  const [isElemsError, setIsElemsError] = useState<boolean>(false);

  const onValidate = useCallback((values: PostDocumentDto) => {
    console.log("Create Document Page values validate");
    console.log(JSON.stringify(values, null, 2));

    let formErrors: CreateErrorObject<PostDocumentDto> = {};

    formErrors.name = validateStraight(
      yup
        .string()
        .max(255, "Назва повинна бути не більше 255 символів")
        .required("Обов'язково"),
      values.name
    );
    formErrors.projectAim = validateStraight(
      yup
        .string()
        .max(255, "Опис повинен бути не більше 255 символів")
        .required("Обов'язково"),
      values.projectAim
    );

    console.log('@formErrors');
    console.log(JSON.stringify(formErrors, null, 2));

    formErrors = _.pickBy(formErrors, _.identity);

    return formErrors;
  }, []);

  const onSubmit = useCallback(
    (values: PostDocumentDto) => {
      const postDocumentDto: PostDocumentDto = {
        name: values.name,
        projectAim: values.projectAim,
        glossaries: values.glossaries,
        userStories: values.userStories,
      };

      console.log("Create Project Page values submit");
      console.log(JSON.stringify(postDocumentDto, null, 2));

      dispatch<PostDocumentAction>({
        type: "POST_DOCUMENT",
        payload: postDocumentDto,
        navigate,
      });
    },
    [dispatch, navigate]
  );

  const postDocument: PostDocumentDto = {
    name: "",
    projectAim: "",
    glossaries: [],
    userStories: [],
  };

  return (
    <PostDocument
      postDocument={postDocument}
      onValidate={onValidate}
      onSubmit={onSubmit}
      glossaries={glossaries}
      userStories={userStories}
      setGlossaries={setGlossaries}
      setUserStories={setUserStories}
      isElemsError={isElemsError}
      setIsElemsError={setIsElemsError}
    />
  );
};

export default PostDocumentPage;
