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
  onLogout: () => void;
}

const ProfileIcon: FC<Props> = (params) => {
  return (
    <ProfileDiv>
        <FieldGrid>
          <Link
            to={params.me ? "/profile" : '/login'}
            style={{
              textDecoration: 'none',
              color: 'white',
            }}
          >
              <Icon type='profile-white' style={{width: 50, height: 50, paddingRight: 10 }}  />
          </Link>
          {params.me ?
            <Button buttonType='button' onClick={() => params.onLogout()}>Вийти</Button>
            :
            <Link
              to={'/login'}
              style={{
                textDecoration: 'none',
                color: 'white',
              }}
            >
              <Button buttonType='button'>Увійти</Button>
            </Link> 
          }
        </FieldGrid>
    </ProfileDiv>
  );
};

export default ProfileIcon;
