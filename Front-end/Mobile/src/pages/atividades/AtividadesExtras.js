import React, { Component } from 'react';
import {
    StyleSheet,
    Text,
    TouchableOpacity,
    View,
    Image,
    ImageBackground,
    TextInput,
    Modal,
    AnimatableBlurView,
    FlatList,
    SectionList,
    SafeAreaView,
    ScrollView,
    Pressable
} from 'react-native';

import AppLoading from 'expo-app-loading';
import * as Font from 'expo-font';
import AsyncStorage from '@react-native-async-storage/async-storage';
import api from '../../services/apiGp1'
import base64 from 'react-native-base64';
import { EvilIcons, AntDesign, MaterialCommunityIcons, FontAwesome5 } from "@expo/vector-icons";
// import 'intl';

let customFonts = {
    'Montserrat-Regular': require('../../../assets/fonts/Montserrat-Regular.ttf'),
    'Montserrat-Bold': require('../../../assets/fonts/Montserrat-Bold.ttf'),
    'Montserrat-SemiBold': require('../../../assets/fonts/Montserrat-SemiBold.ttf'),
    'Montserrat-Medium': require('../../../assets/fonts/Montserrat-Medium.ttf'),
    'Quicksand-Regular': require('../../../assets/fonts/Quicksand-Regular.ttf'),
    'Quicksand-SemiBold': require('../../../assets/fonts/Quicksand-SemiBold.ttf')
}

export default class AtividadesExtras extends Component {

    constructor(props) {
        super(props);
        this.state = {
            listaAtividades: [],
            AtividadeBuscada: {},
            modalVisible: false,
        };
    }

    buscarAtividade = async () => {
        const resposta = await api.get('/Atividades/ListarExtras');
        const dadosDaApi = resposta.data;
        this.setState({ listaAtividades: dadosDaApi });
    };


    ProcurarAtividades = async (id) => {
        //console.warn(id)
        try {

            const resposta = await api('/Atividades/' + id);
            if (resposta.status == 200) {
                const dadosAtividades = await resposta.data.atividade;
                await this.setState({ AtividadeBuscada: dadosAtividades })
                

            }
        }
        catch (erro) {
            console.warn(erro);
        }
    }


    setModalVisible = async (visible, id) => {
        if (visible == true) {
            //console.warn(id)
            await this.ProcurarAtividades(id)
            this.setState({ modalVisible: true });
            //console.warn(this.state.AtividadeBuscada)
        }
        else if (visible == false) {
            this.setState({ AtividadeBuscada: {} })
            this.setState({ modalVisible: false })
        }

    }


    async _loadFontsAsync() {
        await Font.loadAsync(customFonts);
        this.setState({ fontsLoaded: true });
    }

    componentDidMount() {
        this._loadFontsAsync();
        this.buscarAtividade();
    }

    associar = async (item) => {
        var Buffer = require('buffer/').Buffer
        try {
            console.warn(item)
            const token = (await AsyncStorage.getItem('userToken'));
            let base64Url = token.split('.')[1]; // token you get
            let base64 = base64Url.replace('-', '+').replace('_', '/');
            let decodedData = JSON.parse(Buffer.from(base64, 'base64').toString('binary'));
            //const xambers = JSON.parse(atob(token.split('.')[1]))
            console.warn(decodedData);

            const resposta = await api.post(
                '/Atividades/Associar/' + decodedData.jti + '/' + item,
                {

                },
                {
                    headers: {
                        Authorization: 'Bearer ' + token,
                    },
                },

                
                
                );
                if (resposta.status == 200) {
                    console.warn(resposta)
                //console.warn('Voce se associou a uma atividade');
            } else {
                //console.warn('Falha ao se associar.');
            }
        } catch (error) {
            console.warn(error);
        }
    }

    render() {
        if (!customFonts) {
            return <AppLoading />;
        }
        return (

            <View style={styles.main}>

             <View>
                    <View style={styles.mainHeader}>
                        <Image source={require('../../../assets/img-gp1/logoSenai2.png')}
                            style={styles.imgLogo}
                        />
                    </View>

                    <View style={styles.titulo}>

                        <Text style={styles.tituloEfects}>{'atividades'.toUpperCase()} </Text>

                        <View style={styles.escritaEscolha}>
                            <View style={styles.itemEquipe}>
                                <Pressable onPress={() => this.props.navigation.navigate('Atividades')}>
                                    <Text style={styles.font}> Obrigatórios </Text>
                                    <View style={styles.line1}></View>
                                </Pressable>

                            </View>

                            <View style={styles.itemIndividual}>
                                <Pressable>
                                    <Text style={styles.font}> Extras </Text>
                                </Pressable>
                                <View style={styles.line2}></View>
                            </View>

                        </View>
                    </View>
                </View>

                <FlatList
                    // contentContainerStyle={styles.boxAtividade}
                    // style={styles.boxAtividade}
                    data={this.state.listaAtividades}
                    keyExtractor={item => item.idAtividade}
                    renderItem={this.renderItem} />

            </View>

        )
    }

    renderItem = ({ item }) => (


        <View style={styles.boxAtividade}>
            <View style={styles.box}>
                <View style={styles.quadrado}></View>
                <View style={styles.espacoPontos}>
                    <Text style={styles.pontos}> {item.recompensaMoeda} Cashs </Text>
                    <FontAwesome5 name="coins" size={24} color="black" />
                </View>
                <View style={styles.conteudoBox}>
                    <Text style={styles.nomeBox}> {item.nomeAtividade} </Text>

                    <Text style={styles.criador}> Responsável: {item.idGestorCadastroNavigation.nome} </Text>
                    <Text style={styles.data}> Item Postado: {Intl.DateTimeFormat("pt-BR", {
                    year: 'numeric', month: 'short', day: 'numeric',
                }).format(new Date(item.dataCriacao))} </Text>
                </View>

                <View style={styles.ModaleBotao}>
                    <Pressable style={styles.botao}
                        onPress={() => this.associar(item.idAtividade)}
                    >
                        <View style={styles.corBotão}>

                            <Text style={styles.texto}> Me Associar </Text>
                        </View>
                    </Pressable>

                    <Pressable style={styles.Modalbotao} onPress={() => this.setModalVisible(true, item.idAtividade)}  >

                        <AntDesign name="downcircleo" size={24} color="#636466" />

                        
                    </Pressable>
                </View>

            </View>

            <Modal
                animationType="fade"
                transparent={true}
                visible={this.state.modalVisible}
                key={item.idAtividade == this.state.AtividadeBuscada.idAtividade}
                onRequestClose={() => {
                    console.warn(item)
                    this.setModalVisible(!this.state.modalVisible)
                }}
            >


                <View style={styles.centeredView}>
                    <View style={styles.modalView}>
                        <View style={styles.quadradoModal}></View>
                        <View style={styles.conteudoBoxModal}>
                            <Text style={styles.nomeBoxModal}>{this.state.AtividadeBuscada.nomeAtividade} </Text>
                            <Text style={styles.descricaoModal}> {this.state.AtividadeBuscada.descricaoAtividade}</Text>
                            <Text style={styles.itemPostadoModal}> Item Postado: {this.state.AtividadeBuscada.dataCriacao} </Text>
                            <Text style={styles.entregaModal}> Data de Entrega: {this.state.AtividadeBuscada.dataConclusao} </Text>
                            <Text style={styles.pessoasModal}> Em {this.state.AtividadeBuscada.equipe} </Text>
                            <Text style={styles.criadorModal}> Responsável: {this.state.AtividadeBuscada.criador} </Text>
                        </View>
                        <View style={styles.botoesModal}  >
                            <Pressable  onPress={() => this.associar(this.state.AtividadeBuscada.idAtividade)} >
                                <View style={styles.associarModal}>
                                    <Text style={styles.texto}> Me Associar </Text>
                                </View>
                            </Pressable>
                            <Pressable

                                onPress={() => this.setModalVisible(!this.state.modalVisible)}
                            >
                                <View style={styles.fecharModal}>
                                    <Text style={styles.textoFechar}>Fechar X</Text>
                                </View>

                            </Pressable>
                        </View>
                    </View>

                </View>

            </Modal>
        </View>

    )

};
const styles = StyleSheet.create({

    main: {
        flex: 1,
        backgroundColor: '#F2F2F2',
    },

    mainHeader: {

        alignItems: 'center',
        paddingTop: 40,

    },

    titulo: {
        justifyContent: 'center',
        alignItems: 'center',
        paddingTop: 40,
    },

    tituloEfects: {
        fontFamily: 'Montserrat-SemiBold',
        justifyContent: 'center',
        alignItems: 'center',
        color: '#2A2E32',
        fontSize: 30,
    },

    escritaEscolha: {
        justifyContent: 'center',
        alignItems: 'center',
        flexDirection: 'row',
        paddingTop: 30,
        // paddingBottom:20
    },

    itemEquipe: {

        marginRight: 80,
        alignItems: 'center',
    },

    font: {
        fontFamily: 'Quicksand-Regular',
        fontSize: 20,
        paddingBottom: 5,
    },

    line1: {
        width: '100%',
        borderBottomWidth: 1,
    },

    itemIndividual: {
        alignItems: 'center',
    },

    line2: {
        width: '100%',
        borderBottomWidth: 1,
    },

    boxAtividade: {

        paddingTop: 40,

        alignItems: 'center',
    },

    quadrado: {
        backgroundColor: '#2A2E32',
        height: 28,
        width: '100%',
        borderTopRightRadius: 8,
        borderTopLeftRadius: 8,

    },




    box: {
        height: 210,
        borderWidth: 1,
        borderColor: '#B3B3B3',
        backgroundColor: '#F2F2F2',
        borderRadius: 10,
        marginBottom: 40,
        width: '85%',
    },

    espacoPontos: {
        flexDirection: 'row',
        justifyContent: 'flex-end',
        paddingTop: 10,
        paddingRight: 18,
    },

    pontos: {
        fontSize: 14,
        paddingRight: 5,
        fontFamily: 'Quicksand-SemiBold',
    },

    imgCoins: {
        width: 18,
        height: 18,
    },

    conteudoBox: {
        paddingLeft: 15,
    },


    nomeBox: {
        fontFamily: 'Quicksand-SemiBold',
        color: '#000000',
        fontSize: 18,
    },

    criador: {
        fontFamily: 'Quicksand-Regular',
        fontSize: 15,

        paddingTop: 8,
    },

    data: {
        fontFamily: 'Quicksand-Regular',
        fontSize: 15,
        paddingTop: 8,
    },
    Modalbotao: {
        paddingRight: 18,
        paddingTop: 15
    },

    botao: {
        // flexDirection: 'row',
        justifyContent: 'flex-start',
        paddingTop: 20,
        paddingLeft: 16
    },

    corBotão: {
        borderRadius: 15,
        height: 30,
        width: 87,
        backgroundColor: '#C20004',
        alignItems: 'center',
        justifyContent: 'center',
    },

    texto: {
        fontFamily: 'Montserrat-Medium',
        color: '#E2E2E2',
        fontSize: 11,
        //alignItems: 'center',
    },

    botaoIndisp: {
        //alignItems: 'center',
        justifyContent: 'center',
        paddingTop: 19,
    },

    corIndisp: {
        borderRadius: 5,
        height: 40,
        width: 90,
        backgroundColor: '#B1B3B6',
        //alignItems: 'center',
        justifyContent: 'center',
    },

    ModaleBotao: {
        flexDirection: 'row',
        justifyContent: 'space-between',
        //alignItems: 'center',


    },

    textoIndisp: {
        // fontFamily: 'Montserrat-SemiBold',
        color: '#000000',
        fontSize: 11,
        alignItems: 'center',
    },

    centeredView: {
        flex: 1,
        justifyContent: "center",
        alignItems: 'center',
        // marginTop: 22
    },

    modalView: {
        height: 350,
        borderWidth: 1,
        borderColor: '#B3B3B3',
        backgroundColor: '#F2F2F2',
        borderRadius: 10,
        // marginBottom: 20,
        width: '78%',

    },

    quadradoModal: {
        backgroundColor: '#2A2E32',
        height: 35,
        width: '100%',
        borderTopRightRadius: 8,
        borderTopLeftRadius: 8

    },
    nomeBoxModal: {
        fontFamily: 'Quicksand-SemiBold',
        textAlign: "center",
        paddingTop: 24,
        fontSize: 20

    },

    descricaoModal: {
        fontFamily: 'Quicksand-Regular',
        paddingTop: 24,
        fontSize: 15,
        paddingBottom: 16,
        marginLeft: 16
    },

    itemPostadoModal: {
        fontFamily: 'Quicksand-Regular',
        fontSize: 15,
        paddingBottom: 16,
        marginLeft: 16
    },

    entregaModal: {
        fontFamily: 'Quicksand-Regular',
        fontSize: 15,
        paddingBottom: 16,
        marginLeft: 16
    },

    pessoasModal:{
        fontFamily: 'Quicksand-Regular',
        fontSize: 15,
        paddingBottom: 16,
        marginLeft: 16
    },

    criadorModal: {
        fontFamily: 'Quicksand-Regular',
        fontSize: 15,
        paddingBottom: 30,
        marginLeft: 16
    },

    botoesModal: {
        fontFamily: 'Montserrat-Medium',
        flexDirection: 'row',
        justifyContent: 'center',
        justifyContent: 'space-evenly'
    },

    associarModal: {
        borderRadius: 15,
        height: 30,
        width: 108,
        backgroundColor: '#C20004',
        alignItems: 'center',
        justifyContent: 'center',
    },

    fecharModal: {
        borderRadius: 15,
        height: 30,
        width: 108,
        alignItems: 'center',
        justifyContent: 'center',
        borderWidth: 1,
        borderColor: '#C20004',
        color: '#C20004'
    },

    textoFechar: {
        fontFamily: 'Montserrat-Medium',
        color: '#C20004'
    }



})