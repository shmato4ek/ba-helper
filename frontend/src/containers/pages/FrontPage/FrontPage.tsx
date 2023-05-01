

import React, { FC } from 'react';
import styled from 'styled-components';
import Button from '../../../components/Button/Button';

const FrontPageStyled = styled.div`
  display: grid;
  grid-template-rows: 100px 1fr 100px;
  grid-template-columns: 100px 1fr 1fr 1fr 100px;
  grid-template-areas:
    'top top top top top'
    'left form features features right'
    'bottom bottom bottom bottom bottom';
  gap: 0px;
  background-color: #EEEEEE;
`;

const Form = styled.div`
  grid-area: form;
  height: 100%;
  background-color: rgba(126, 211, 135, 0.58);
  padding: 90px 30px;
  box-sizing: border-box;

  font-family: Roboto;
  font-size: 24px;
  font-weight: 400;
  line-height: 42px;
  letter-spacing: 0.065em;
  text-align: left;

`;

const FeaturesList = styled.div`
  grid-area: features;
  height: 100%;
`;

const CenterHorizontalDiv = styled.div`
  display: flex;
  justify-content: center;
`;

type Props = {
}

const FrontPage: FC<Props> = (params) => {
  return (
    <FrontPageStyled>
      <Form>
        <div>
          Веб-додаток для бізнес аналітиків допомагає створювати документацію за шаблоном,
          створювати та організовувати проекти, створювати розклад та завдання. Він є
          корисним інструментом для організації робочих процесів та успішного завершення
          проектів.
        </div>
        <CenterHorizontalDiv>
          <Button buttonType='button' styleType='simple'>Почати Зараз</Button>
        </CenterHorizontalDiv>
      </Form>
      <FeaturesList />
      {/* <Title>Менеджер проекту</Title> */}
    </FrontPageStyled>
  );
};

export default FrontPage;

