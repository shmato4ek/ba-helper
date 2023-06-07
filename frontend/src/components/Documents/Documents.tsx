import React, {FC, useCallback} from 'react';
import { DocumentDto, ProjectDtoFields } from '../../store/types';
import { Link } from 'react-router-dom';
import Button from '../Button/Button';
import { AlignCenter } from '../Utils/Utils';
import { ProjectsStyled, Table, TD, TH, TR } from '../Projects/Projects';
import {DeleteDocument, DeleteTask} from "../../store/actions";
import Icon from "../Icon/Icon";
import {useDispatch} from "react-redux";
import styled from "styled-components";

const THSmall = styled.th`
  border: 1px solid #ddd;
  text-align: left;
  border-bottom: 1px solid #296A2F;
  
  width: 30px;
`

type Props = {
  documents: DocumentDto[];
  optionalFields: ProjectDtoFields[];

  onDocumentDownload: (documentId: number) => any;
}

const documentFieldInfo = {
  [ProjectDtoFields.hours]: 'Кількість годин',
  [ProjectDtoFields.authorName]: 'Затверджувач',
  [ProjectDtoFields.taskCount]: 'Кількість завдань',
}

const Documents: FC<Props> = (params) => {
    const dispatch = useDispatch();

    const onDelete = (id: number) => {
        console.log('Document deleted');

        dispatch<DeleteDocument>({
            type: 'DELETE_DOCUMENT',
            payload: {
                documentId: id
            }
        })
    };

  return (
    <ProjectsStyled>
      <Table>
        <thead>
          <TR>
            <TH>Назва</TH>
            <TH>Ціль проекту</TH>
            <TH>Скачати</TH>
            <THSmall></THSmall>
          </TR>
        </thead>
        <tbody>
          {params.documents.map(document => {
            return (
              <TR key={document.id}>
                <TD>{document.name}</TD>
                <TD>{document.projectAim}</TD>
                <TD width={150}>
                  <Button buttonType='button' styleType='simple' onClick={() => {
                    params.onDocumentDownload(document.id)
                  }}>
                    Скачати
                  </Button>
                </TD>
                  <TD>
                      <Button buttonType='button' styleType='none' onClick={ () => {
                          onDelete(document.id)
                      }}>
                          <Icon type='trash-can' style={{width: 30, height: 30 }} />
                      </Button>
                  </TD>
              </TR>
            )
          })}
        </tbody>
      </Table>
      <AlignCenter>
        <Button buttonType='button' styleType='simple'>
          <Link
            to="/documents/new"
            style={{
              textDecoration: 'none',
              color: 'white',
            }}
          >Створити</Link>
        </Button>
      </AlignCenter>
    </ProjectsStyled>
  );
};

export default Documents;
