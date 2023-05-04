import React, { FC } from 'react';
import styled from 'styled-components';
import Icon from '../Icon/Icon';
import Button from '../Button/Button';
import { Link } from 'react-router-dom';
import { Me } from '../../store/types';
import { FieldGrid } from '../Project/Project';

const ProfileDiv = styled.div`
  border-bottom: 3px #0FB800;
  background-color: #ffffff;

  text-align: center;
  width: 100%;
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
`;

type Props = {
  me?: Me | null;
}

const ProfileIcon: FC<Props> = (params) => {
  return (
    <ProfileDiv>
      <Link
        to={params.me ? "/profile" : '/login'}
        style={{
          textDecoration: 'none',
          color: 'white',
        }}
      >
        <FieldGrid>
          <Icon type='profile-white' style={{width: 50, height: 50, paddingRight: 10 }}  />
          <Button buttonType='button'>Увійти</Button>
        </FieldGrid>
      </Link>
    </ProfileDiv>
  );
};

export default ProfileIcon;
