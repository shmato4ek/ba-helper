import React, { FunctionComponent } from 'react';
import Icon from '../Icon/Icon';

import styles from './Button.module.scss';

interface Props {
  buttonType: 'button' | 'reset' | 'submit';
  styleType?: 'simple' | 'desaturated' | 'outlined';
  onClick?: () => any;
  children: string | React.ReactNode;
}

const Button: FunctionComponent<Props> = ({
  buttonType,
  styleType,
  onClick,
  children,
}: Props) => {
  return (
    <button
      type={buttonType}
      onClick={onClick}
      className={`${styles.button} ${styleType === 'desaturated' && styles.desaturated_button} ${styleType === 'outlined' && styles.outlined_button}`}
    >
      <div className={`${styles.button_label} ${styleType === 'desaturated' && styles.desaturated_button_label} ${styleType === 'outlined' && styles.outlined_button_label}`}>{children}</div>
    </button>
  );

};

Button.defaultProps = {
  styleType: 'simple',
  onClick: undefined,
};

export default Button;
