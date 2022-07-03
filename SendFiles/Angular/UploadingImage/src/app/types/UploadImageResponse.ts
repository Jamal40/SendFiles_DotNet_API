import { UploadFileResponseCodes } from './UploadFileResponseCodes';

export default class UploadFileResponse {
  responseCode: UploadFileResponseCodes = UploadFileResponseCodes.Success;
  url: string = '';
}
