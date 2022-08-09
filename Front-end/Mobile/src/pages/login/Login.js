import React, { Component, useState } from "react";

import {
  StyleSheet,
  Text,
  TouchableOpacity,
  View,
  Image,
  TextInput,
  Animated,
  Alert,
  ColorPropType,
} from "react-native";

import AsyncStorage from "@react-native-async-storage/async-storage";
import * as Font from "expo-font";
import AppLoading from "expo-app-loading";
import jwt_decode from "jwt-decode";
import api from "../../services/api";
import AwesomeAlert from "react-native-awesome-alerts";
import AnimatedInput from "react-native-animated-input";
import axios from "axios";
import { Colors } from "react-native/Libraries/NewAppScreen";
import { TextInputMask } from "react-native-masked-text";
import { Ionicons } from "@expo/vector-icons";

let customFonts = {
  "Montserrat-Regular": require("../../../assets/fonts/montserrat/Montserrat-Regular.ttf"),
  "Montserrat-Medium": require("../../../assets/fonts/montserrat/Montserrat-Medium.ttf"),
  "Montserrat-Bold": require("../../../assets/fonts/montserrat/Montserrat-Bold.ttf"),
  "Quicksand-Regular": require("../../../assets/fonts/quicksand/Quicksand-Regular.ttf"),
};

export default class Login extends Component {
  constructor(props) {
    super(props);
    this.state = {
      cpf: "71696553067",
      //cpf: "11111111176",
      senha: "Sesisenai@2022",
      fontsLoaded: false,
      error: "Email ou Senha inválidos!",
      setLoading: false,
      showAlert: false,
      hidePass: true,
    };
  }

  showAlert = () => {
    this.setState({ showAlert: true });
  };

  hideAlert = () => {
    this.setState({
      showAlert: false,
    });
  };

  async _loadFontsAsync() {
    await Font.loadAsync(customFonts);
    this.setState({ fontsLoaded: true });
  }

  componentDidMount() {
    this._loadFontsAsync();
  }

  realizarLogin = async () => {
    try {
      const resposta = await api.post("/Login", {
        cpf: this.state.cpf,
        senha: this.state.senha,
      });

      console.warn(resposta);
      const token = resposta.data.token;

      console.warn(token);

      await AsyncStorage.setItem("userToken", token);
      console.warn(resposta.data);

      if (resposta.status === 200) {
        console.warn("Login Realizado");
        var certo = jwt_decode(token).role;

        this.props.navigation.navigate("Redirecionar");
      }
    } catch (error) {
      console.warn(error);
      this.showAlert();
    }
  };

  render() {
    if (!this.state.fontsLoaded) {
      return <AppLoading />;
    }

    return (
      <View style={styles.body}>
        <AwesomeAlert
          show={this.state.showAlert}
          showProgress={false}
          title="Login Inválido!"
          titleStyle={styles.tituloModalLogin}
          message="O CPF ou a senha inserídos são inválidos!"
          messageStyle={styles.textoModalLogin}
          closeOnTouchOutside={true}
          closeOnHardwareBackPress={false}
          confirmButtonStyle={styles.confirmButton}
          showCancelButton={false}
          showConfirmButton={true}
          confirmText="Voltar"
          confirmButtonColor="#C20004"
          onConfirmPressed={() => {
            this.hideAlert();
          }}
        />
        <View style={styles.mainHeader}>
          <Image
            source={require("../../../assets/imgMobile/logo_2S.png")}
            style={styles.imgLogo}
          />
        </View>

        <View style={styles.container}>
          <Text style={styles.tituloPagina}>
            {"recursos humanos".toUpperCase()}
          </Text>

          <View style={styles.viewLoginCPF}>
            <TextInputMask
              style={styles.inputLogin}
              placeholder="CPF"
              type={"cpf"}
              value={this.state.value}
              keyboardType="numeric"
              placeholderTextColor="#B3B3B3"
              onChangeText={(cpf) => this.setState({ cpf })}
            />
          </View>

          <View style={styles.TextEmail}>
            <TextInput
              style={styles.inputLogin}
              placeholder="Senha"
              placeholderTextColor="#B3B3B3"
              keyboardType="default"
              onChangeText={(senha) => this.setState({ senha })}
              secureTextEntry={true}
              value={this.state.value}
            
            />
          </View>

          <View style={styles.erroMsg}>
            <TouchableOpacity
              onPress={() => this.props.navigation.navigate("alterarSenha")}
            >
              <Text style={styles.textEsque}> Esqueci a Senha</Text>
            </TouchableOpacity>
          </View>

          <TouchableOpacity
            style={styles.btnLogin}
            onPress={this.realizarLogin}
          >
            <Text style={styles.btnText}>Entrar</Text>
          </TouchableOpacity>
        </View>
        <View style={styles.imgLoginView}>
          <Image
            source={require("../../../assets/imgMobile/imagemLogin.png")}
          />
        </View>
      </View>
    );
  }
}

const styles = StyleSheet.create({
  body: {
    backgroundColor: "#F2F2F2",
  },

  mainHeader: {
    paddingTop: 40,
    alignItems: "center",
    justifyContent: "center",
  },

  imgLogo: {
    width: 224,
    height: 31,
  },

  container: {
    alignItems: "center",
  },

  tituloPagina: {
    fontFamily: "Montserrat-Bold",
    fontSize: 30,
    color: "#2A2E32",
    width: 175,
    paddingTop: 64,
    paddingBottom: 50,
    alignItems: "center",
  },
  tituloModalLogin: {
    color: "#C20004",
    fontFamily: "Montserrat-Medium",
    fontSize: 23,
    fontWeight: "bold",
  },
  textoModalLogin: {
    width: 200,
    textAlign: "center",
  },
  confirmButton: {
    width: 100,

    paddingLeft: 32,
  },

  inputLogin: {
    width: 350,
    height: 46,
    borderWidth: 1,
    borderColor: "#B3B3B3",
    alignItems: "center",
    justifyContent: "center",
    borderRadius: 10,
    fontSize: 14,
    flexDirection: "column",
    paddingLeft: 15,
  },

  viewLoginCPF: {
    marginBottom: 24,
  },

  erroMsg: {
    paddingTop: 24,
    alignItems: "center",
    justifyContent: "center",
    flexDirection: "row",
    marginLeft: 250,
  },

  erroText: {
    fontFamily: "Quicksand-Regular",
    fontSize: 12,
    color: "#C20004",
    paddingRight: 115,
  },

  textEsque: {
    fontFamily: "Quicksand-Regular",
    fontSize: 12,
    color: "#C20004",
  },

  btnLogin: {
    width: 350,
    height: 46,
    fontSize: 20,
    borderRadius: 5,
    alignItems: "center",
    justifyContent: "center",
    marginTop: 24,
    elevation: 16,
    backgroundColor: "#C20004",
    borderRadius: 10,
  },

  btnText: {
    fontFamily: "Montserrat-Regular",
    fontSize: 12,
    color: "#F2F2F2",
    alignItems: "center",
    justifyContent: "center",
  },

  imgLoginView: {
    //justifyContent:'flex-start',
    marginTop: 92,
    //width: 180,
    //height: 165,
    paddingLeft: 40,
    alignItems: "flex-start",
    flexDirection: "column",
  },
});
