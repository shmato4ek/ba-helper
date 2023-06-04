import React from 'react';
import {
  AddFileIcon,
  ArchiveIcon, ArrowUpIcon,
  CalendarIcon,
  DiagramIcon,
  EditPencilIcon,
  EyeIcon,
  FileIcon,
  GreenPlusIcon,
  LogoIcon,
  PeopleIcon,
  ProfileIcon,
  ProfileWhiteIcon,
  RaciIcon,
  SaveIcon,
  TextFileIcon, TrashCanIcon,
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
  | 'file'
  | 'calendar'
  | 'people'
  | 'profile-white'
  | 'edit-pencil'
  | 'save'
  | 'green-plus'
  | 'triangle'
  | 'archive'
  | 'trash-can'
  | 'arrow-up';
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
    case 'file':
      return <FileIcon style={style} onClick={onClick} />;
    case 'calendar':
      return <CalendarIcon style={style} onClick={onClick} />;
    case 'people':
      return <PeopleIcon style={style} onClick={onClick} />;
    case 'profile-white':
      return <ProfileWhiteIcon style={style} onClick={onClick} />;
    case 'save':
      return <SaveIcon style={style} onClick={onClick} />;
    case 'edit-pencil':
      return <EditPencilIcon style={style} onClick={onClick} />;
    case 'green-plus':
      return <GreenPlusIcon style={style} onClick={onClick} />;
    case 'archive':
      return <ArchiveIcon style={style} onClick={onClick} />;
    case 'trash-can':
      return <TrashCanIcon style={style} onClick={onClick} />;
    case 'arrow-up':
      return <ArrowUpIcon style={style} onClick={onClick} />;
    default:
      return <></>;
  }
};

Icon.defaultProps = {
  onClick: undefined,
};

export default Icon;
