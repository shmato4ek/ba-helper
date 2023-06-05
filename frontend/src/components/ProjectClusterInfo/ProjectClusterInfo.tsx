import React, {FC, useEffect} from 'react';
import { ClusterInfo, ProjectDto, ProjectDtoFields, taskTopicToText } from '../../store/types';
import styled from 'styled-components';
import { DateTime } from 'luxon';
import { Link } from 'react-router-dom';
import Button from '../Button/Button';
import { AlignCenter, HorizontalGrid, VerticalGrid } from '../Utils/Utils';
import { ProjectsStyled, Table, TD, TH, TR } from '../Projects/Projects';

type Props = {
  currentProjectStats: ClusterInfo[];
}

export const GreenHeader = styled.div`
  background-color: rgb(200, 239, 204);
  box-shadow: 2px 2px 2px 1px rgba(0, 0, 0, 0.2);
  
  padding: 20px;
  
  height: 30px;

  text-align: center;
  align-items: center;
  
  font-style: normal;
  font-weight: 500;
  font-size: 24px;
  line-height: 28px;
`;

export const VerticalGridNoGap = styled.div`
  display: grid;
  align-content: start;
`;

const ProjectClusterInfo: FC<Props> = (params) => {

  return (
      <div style={{overflowX: "scroll"}}>
          <ProjectsStyled>
              <HorizontalGrid>
                  {params.currentProjectStats.map((stats, index) => {
                      return (
                          <VerticalGridNoGap>
                              <GreenHeader>Користувачі</GreenHeader>
                              <Table>
                                  <thead>
                                  <TR>
                                      <TH>Ім'я</TH>
                                      <TH>Імейл</TH>
                                  </TR>
                                  </thead>
                                  <tbody>
                                  {stats.users.map(user => {
                                      return (
                                          <TR>
                                              <TD width={'50%'}>{user.name}</TD>
                                              <TD width={'50%'}>{user.email}</TD>
                                          </TR>
                                      )
                                  })}
                                  </tbody>
                              </Table>
                              <GreenHeader>Спеціалізація</GreenHeader>
                              <Table>
                                  <thead>
                                  <TR>
                                      <TH>Тег</TH>
                                      <TH>Якість Виконання</TH>
                                  </TR>
                                  </thead>
                                  <tbody>
                                  {stats.data.map(datum => {
                                      return (
                                          <TR>
                                              <TD width={'50%'}>{taskTopicToText(datum.topic)}</TD>
                                              <TD width={'50%'}>{datum.quality}%</TD>
                                          </TR>
                                      )
                                  })}
                                  </tbody>
                              </Table>
                          </VerticalGridNoGap>
                      )
                  })}
              </HorizontalGrid>
          </ProjectsStyled>
      </div>
  );
};

export default ProjectClusterInfo;
