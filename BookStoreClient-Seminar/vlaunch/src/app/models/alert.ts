export interface Alert {
    type?: string;
    message?: string;
    extra_type?: string;
    error_code?: string;
    callback?: Function;
  }
  