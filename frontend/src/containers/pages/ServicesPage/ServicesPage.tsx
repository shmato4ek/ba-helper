

import React, { FC } from 'react';
import { BisMetriscDto } from '../../../store/types';
import Icon from '../../../components/Icon/Icon';
import styled from 'styled-components';
import Button from '../../../components/Button/Button';
import ProfileContainer from '../../ProfileContainer/ProfileContainer';
import Font from '../../../components/Font/Font';

const FrontPageStyled = styled.div`
  display: grid;
  grid-template-rows: 100px 1fr 100px;
  grid-template-columns: 100px 1fr 1fr 1fr 100px;
  grid-template-areas:
    'top top top top top'
    'left services services services right'
    'bottom bottom bottom bottom bottom';
  gap: 0px;
  background-color: #EEEEEE;
`;

const Services = styled.div`
  grid-area: services;
  height: 100%;
  box-sizing: border-box;

  font-family: Roboto;
  font-size: 24px;
  font-weight: 400;
  line-height: 42px;
  letter-spacing: 0.065em;
  text-align: left;
`;

const GreenCard = styled.div`
  height: 320px;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;

  padding: 20px;

  background-color: rgba(126, 211, 135, 0.58);
`;

const List = styled.div`
  display: grid;
  grid-gap: 50px;
  grid-auto-flow: column;

  box-sizing: border-box;
`;

const HeaderWrapper = styled.div`
  padding-bottom: 100px;
  text-align: center;
`;

type Props = {
}

const ServicesPage: FC<Props> = (params) => {

  
  return (
    <FrontPageStyled>
      <Services>
        <HeaderWrapper>
          <Font type='h1'>Сервіси</Font>
        </HeaderWrapper>
        <List>
          <Button buttonType='button' styleType='none'>
            <GreenCard>
              <Icon type='add-file'/>
              <div>Створити документацію</div>
            </GreenCard>
          </Button>

          <Button buttonType='button' styleType='none'>
            <GreenCard>
              <Icon type='diagram'/>
              Створити діаграму
            </GreenCard>
          </Button>

          <Button buttonType='button' styleType='none'>
            <GreenCard>
              <Icon type='raci'/>
              Створити RACI матрицю
            </GreenCard>
          </Button>

          <Button buttonType='button' styleType='none'>
            <GreenCard>
              <Icon type='text-file'/>
              Створити план комунікації
            </GreenCard>
          </Button>
        </List>
      </Services>
    </FrontPageStyled>
  );
};

export default ServicesPage;

