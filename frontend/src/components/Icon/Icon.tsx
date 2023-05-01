import React from 'react';
import {
  AddFileIcon,
  DiagramIcon,
  EyeIcon, LogoIcon, ProfileIcon, RaciIcon, TextFileIcon,
} from '../../assets/icons';

interface Props {
  type:
  | 'tick'
  | 'tick-off'
  | 'eye'
  | 'logo'
  | 'profile'
  | 'text-file'
  | 'diagram'
  | 'raci'
  | 'add-file'
  | 'triangle';
  style?: React.CSSProperties;
  onClick?: () => any;
}

const Icon = ({ type, style, onClick }: Props) => {
  switch (type) {
    case 'eye':
      return <EyeIcon style={style} onClick={onClick} />;
    case 'logo':
      return <LogoIcon style={style} onClick={onClick} />;
    case 'profile':
      return <ProfileIcon style={style} onClick={onClick} />;
    case 'text-file':
      return <TextFileIcon style={style} onClick={onClick} />;
    case 'diagram':
      return <DiagramIcon style={style} onClick={onClick} />;
    case 'raci':
      return <RaciIcon style={style} onClick={onClick} />;
    case 'add-file':
      return <AddFileIcon style={style} onClick={onClick} />;
    default:
      return <></>;
  }
};

Icon.defaultProps = {
  onClick: undefined,
};

export default Icon;
