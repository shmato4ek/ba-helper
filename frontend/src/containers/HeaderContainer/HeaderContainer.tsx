import React, { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';

import { AppState } from '../../store/reducer';
import Header from '../../components/Header/Header';

const HeaderContainer = () => {
  const dispatch = useDispatch();
  const state = useSelector((state: AppState) => state);

  useEffect(()=> {
    // 
  }, [dispatch, state.actions.getMe.success]);

  return (
    <Header/>
  );
};

export default HeaderContainer;
