export type TransactionReceipt = {
  transactionHash: string;
  transactionIndex: {
    hexValue: string;
    value: {
      isPowerOfTwo: boolean;
      isZero: boolean;
      isOne: boolean;
      isEven: boolean;
      sign: number;
    };
  };
  blockHash: string;
  blockNumber: {
    hexValue: string;
    value: {
      isPowerOfTwo: boolean;
      isZero: boolean;
      isOne: boolean;
      isEven: boolean;
      sign: number;
    };
  };
  from: string;
  to: string;
  cumulativeGasUsed: {
    hexValue: string;
    value: {
      isPowerOfTwo: boolean;
      isZero: boolean;
      isOne: boolean;
      isEven: boolean;
      sign: number;
    };
  };
  gasUsed: {
    hexValue: string;
    value: {
      isPowerOfTwo: boolean;
      isZero: boolean;
      isOne: boolean;
      isEven: boolean;
      sign: number;
    };
  };
  effectiveGasPrice: {
    hexValue: string;
    value: {
      isPowerOfTwo: boolean;
      isZero: boolean;
      isOne: boolean;
      isEven: boolean;
      sign: number;
    };
  };
  contractAddress: string;
  status: {
    hexValue: string;
    value: {
      isPowerOfTwo: boolean;
      isZero: boolean;
      isOne: boolean;
      isEven: boolean;
      sign: number;
    };
  };
  logs: string[][];
  type: {
    hexValue: string;
    value: {
      isPowerOfTwo: boolean;
      isZero: boolean;
      isOne: boolean;
      isEven: boolean;
      sign: number;
    };
  };
  logsBloom: string;
  root: string;
};
