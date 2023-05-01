

import React, { FC } from 'react';
import { BisMetriscDto } from '../../store/types';
import Icon from '../Icon/Icon';
import styled from 'styled-components';
import Button from '../Button/Button';
import ProfileContainer from '../../containers/ProfileContainer/ProfileContainer';
import { Link } from 'react-router-dom';

const HeaderStyled = styled.header`
  grid-area: header;

  display: grid;
  grid-gap: 0px;
  grid-auto-flow: column;

  box-sizing: border-box;

  height: 120px;
  padding: 20px;
  border-bottom: 5px solid #0FB800;
  background-color: #EEEEEE;
`;

type Props = {
}

const Header: FC<Props> = (params) => {
  return (
    <HeaderStyled>
      <Link to='/'>
        <Icon type='logo' />
      </Link>

      <Button buttonType='button' styleType='none'>
        <Link to='services'>
          Сервіси
        </Link>
      </Button> 
      <Button buttonType='button' styleType='none'>
        <Link to='my-projects'>
          Поточні Проекти
        </Link>
      </Button> 
      <Button buttonType='button' styleType='none'>
        <Link to='owned-projects'>
          Керівникам
        </Link>
      </Button>
      <ProfileContainer/>
    </HeaderStyled>
  );
};

export default Header;

