

import React, { FC, useCallback } from 'react';
import { useDispatch } from 'react-redux';
import { useSelector } from 'react-redux';
import { useNavigate } from 'react-router';
import ProfileIcon from '../../components/ProfileIcon/ProfileIcon';
import { AppAction } from '../../store/actions';
import { AppState } from '../../store/reducer';

type Props = {
}

const ProfileContainer: FC<Props> = (params) => {
  const me = useSelector((state: AppState) => state.me);
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const onLogout = useCallback(() => {
    console.log("Log out");
    dispatch<AppAction>({ type: 'LOG_OUT_ENDUSER', navigate });
  }, [dispatch, navigate]);

  return (
    <ProfileIcon
      onLogout={onLogout}
      me={me}
    />
  );
};

export default ProfileContainer;

