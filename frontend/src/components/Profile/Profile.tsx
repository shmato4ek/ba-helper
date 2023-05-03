import React, { FC } from 'react';
import styled from 'styled-components';
import Icon from '../Icon/Icon';
import Button from '../Button/Button';

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
}

const Profile: FC<Props> = (params) => {
  return (
    <ProfileDiv>
      <Icon type='profile' style={{width: 50, height: 50, paddingRight: 10 }}  />
      <Button buttonType='button'>Увійти</Button> 
    </ProfileDiv>
  );
};

export default Profile;
