import React, { FC } from 'react';
import { DocumentDto, ProjectDtoFields } from '../../store/types';
import { Link } from 'react-router-dom';
import Button from '../Button/Button';
import { AlignCenter } from '../Utils/Utils';
import { ProjectsStyled, Table, TD, TH, TR } from '../Projects/Projects';


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
  return (
    <ProjectsStyled>
      <Table>
        <thead>
          <TR>
            <TH>Назва</TH>
            <TH>Ціль проекту</TH>
            <TH>Скачати</TH>
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
