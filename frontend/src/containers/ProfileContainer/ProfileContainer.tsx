

import React, { FC } from 'react';
import { useSelector } from 'react-redux';
import ProfileIcon from '../../components/ProfileIcon/ProfileIcon';
import { AppState } from '../../store/reducer';

type Props = {
}

const ProfileContainer: FC<Props> = (params) => {
  const me = useSelector((state: AppState) => state.me);

  return (
    <ProfileIcon
      me={me}
    />
  );
};

export default ProfileContainer;

