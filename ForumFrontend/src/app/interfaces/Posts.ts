import { Metadata } from "./Metadata";
import { Post } from "./Post";

export interface Posts{
  posts: Post[];
  metaData: Metadata;
}
