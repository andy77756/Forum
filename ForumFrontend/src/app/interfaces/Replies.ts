import { Metadata } from './Metadata';
import { Reply } from './Reply';
import { Post } from './Post';
export interface Replies{
  post : Post;
  replies : Reply[];
  metaData : Metadata;
}
