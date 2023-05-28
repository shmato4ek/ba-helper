import React, { FC } from 'react';
import { ClusterInfo, ProjectDto, ProjectDtoFields, StatisticDataInfo, taskTopicToText } from '../../store/types';
import styled from 'styled-components';
import { AlignCenter } from "../Utils/Utils"
import { ProjectsStyled, Table, TD, TH, TR } from '../Projects/Projects';

type Props = {
  meStatistics: StatisticDataInfo[];
}

export const NarrowTable = styled.table`
  font-family: Arial, Helvetica, sans-serif;
  border-collapse: collapse;

  width: 50%;

  box-shadow: 2px 2px 2px 1px rgba(0, 0, 0, 0.2);

  margin-bottom: 20px;

  overflow: scroll;
`;

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
      <AlignCenter>
        <NarrowTable>
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
                <TD>{isNaN(stats.taskQuality) ? "-" : `${Math.floor(stats.taskQuality)}%`}</TD>
                <TD>{isNaN(stats.taskCount) ? "0" : stats.taskCount}</TD>
              </TR>
            )
          })}
        </tbody>
      </NarrowTable>
      </AlignCenter>
    </ProjectsStyled>
  );
};

export default UserStatistics;
