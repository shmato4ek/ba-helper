import React, { useCallback, useEffect } from 'react';
import { useDispatch } from 'react-redux';

import * as yup from 'yup'
import { validateStraight } from '../../../yup';
import * as _ from 'lodash'
import Profile from '../../../components/Profile/Profile';
import { CreateErrorObject, EditPutUserDto, PutUserDto } from '../../../store/types';
import { AppAction, DeleteUser, PutUser } from '../../../store/actions';
import { AppState } from '../../../store/reducer';
import { useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import NotFound from '../../../components/NotFound/NotFound';
import UserStatistics from '../../../components/UserStatistics/UserStatistics';

const ProfileStatisticsPage = () => {
  const dispatch = useDispatch();
  const meStatistics = useSelector((state: AppState) => state.meStatistics);
  const getMeStatisticsAction = useSelector((state: AppState) => state.actions.getMeStatistics);

  console.log('@meStatistics');
  console.log(JSON.stringify(meStatistics, null, 2));

  useEffect(()=> {
    if(!getMeStatisticsAction.loading && !meStatistics) {
      dispatch<AppAction>({ type: 'GET_ME_STATISTICS' });
    }
  }, [dispatch, getMeStatisticsAction, meStatistics]);

  if(!meStatistics) {
    return <NotFound />
  }

  return (
    <UserStatistics 
      meStatistics={meStatistics}
    />
  );
};

export default ProfileStatisticsPage;
