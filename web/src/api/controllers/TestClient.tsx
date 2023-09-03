import Client from "../Client"

const controllerName = "Test";

const ping = async (): Promise<string> => {
    return (await Client.get(`${controllerName}/ping`)).data;
};

const pingAuth = async (): Promise<string> => {
  return ( await  Client.get(`${controllerName}/pingAuth`)).data;
};

export {ping, pingAuth};
