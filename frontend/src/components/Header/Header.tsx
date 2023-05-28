

import React, { FC } from 'react';
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
  background-color: #ffffff;
`;

type Props = {
}

export const HeaderLink: FC<{
  link: string;
  children: JSX.Element[] | JSX.Element | string;
}> = ({
  link,
  children
}) => {
  return <Link to={link} style={{
    textDecoration: 'none',
    color: 'black',
    fontFamily: 'Roboto',
    fontSize: '32px',
    fontWeight: 800,
    lineHeight: '38px',
    letterSpacing: '0.065em',
    textAlign: 'left',
  }}>
    {children}
  </Link>
}

const Header: FC<Props> = (params) => {
  return (
    <HeaderStyled>
      <Link to='/'>
        <Icon type='logo' />
      </Link>
      {/* <Button buttonType='button' styleType='none'>
        <HeaderLink link='services'>Сервіси</HeaderLink>
      </Button>  */}
      <Button buttonType='button' styleType='none'>
        <HeaderLink link='my-projects'>Поточні проєкти</HeaderLink>
      </Button> 
      <Button buttonType='button' styleType='none'>
        <HeaderLink link='owned-projects'>Керівникам</HeaderLink>
      </Button>
      <Button buttonType='button' styleType='none'>
        <HeaderLink link='documents'>Документи</HeaderLink>
      </Button>
      <ProfileContainer/>
    </HeaderStyled>
  );
};

export default Header;

