import React, { useCallback, useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';

import { AppState } from '../../../store/reducer';
import { ProjectDtoFields } from '../../../store/types';
import Loading from '../../../components/Loading/Loading';
import { AppAction, DocumentDownload } from '../../../store/actions';
import Documents from '../../../components/Documents/Documents';

const DocumentsPage = () => {
  const dispatch = useDispatch();
  const documents = useSelector((state: AppState) => state.documents);
  const getDocumentsAction = useSelector((state: AppState) => state.actions.getDocuments);

  useEffect(()=> {
    if(!getDocumentsAction.loading && !documents) {
      dispatch<AppAction>({ type: 'GET_DOCUMENTS' });
    }
  }, [dispatch, documents, getDocumentsAction.loading]);

  const onDocumentDownload = useCallback((documentId: number) => {
    console.log('Delete user');

    dispatch<DocumentDownload>({
      type: 'DOCUMENT_DOWNLOAD',
      payload: documentId
    });
  }, [dispatch]);

  if(!documents) {
    return  <Loading />
  }

  return (
    <Documents
      documents={documents}
      optionalFields={[ProjectDtoFields.authorName, ProjectDtoFields.hours]}

      onDocumentDownload={onDocumentDownload}
    />
  );
};

export default DocumentsPage;
