import React, { FC } from 'react';
import { ClusterInfo, ProjectDto, ProjectDtoFields, StatisticDataInfo, taskTopicToText } from '../../store/types';
import styled from 'styled-components';
import { HorizontalGrid } from '../Utils/Utils';
import { ProjectsStyled, Table, TD, TH, TR } from '../Projects/Projects';

type Props = {
  meStatistics: StatisticDataInfo[];
}

export const GreenHeader = styled.div`
  background-color: rgb(200, 239, 204);
  padding: 20px;

  text-align: center;
  align-items: center;
  
  font-style: normal;
  font-weight: 500;
  font-size: 24px;
  line-height: 28px;
`;

export const VerticalGridNoGap = styled.div`
  display: grid;
`;

const UserStatistics: FC<Props> = (params) => {
  return (
    <ProjectsStyled>
      <Table>
        <thead>
          <TR>
            <TH>Назва тегу завдання</TH>
            <TH>Якість виконання</TH>
            <TH>Кількість виконаних завдань</TH>
          </TR>
        </thead>
        <tbody>
          {params.meStatistics.map(stats => {
            return (
              <TR>
                <TD>{taskTopicToText(stats.taskTopic)}</TD>
                <TD>{Math.floor((stats.taskQuality) * 100)}%</TD>
                <TD>{stats.taskCount}</TD>
              </TR>
            )
          })}
        </tbody>
      </Table>
    </ProjectsStyled>
  );
};

export default UserStatistics;
