import React, { Component } from 'react'
import PropTypes from 'prop-types'
import {
  Text,
  TextInput,
  TouchableOpacity,
  View,
} from 'react-native'
import styles from './styles'

export default class TextInputTest extends Component {
  static propTypes = {
    logger: PropTypes.func
  }

  constructor(props) {
    super(props)

    this.autoCapitalize = ['characters', 'words', 'sentences', 'none']

    this.state = {
      autoCapitalizeMode: this.autoCapitalize[3],
      pressed: null,
      blurOnSubmit: false
    }
  }

  keyPressedHandler = (e) => {
    const { nativeEvent: { key } } = e
    this.setState(previousState => ({
      pressed: key
    }))
    //this.props.logger(`Pressed ${key}`)
  }

  selectCapMode = element =>
    this.setState(previousState => ({
      autoCapitalizeMode: element
    }))
  
    toggleBlurOnSubmit = () => 
    this.setState(previousState => ({
      blurOnSubmit: !previousState.blurOnSubmit
    }))

  render() {
    const { autoCapitalizeMode, pressed, blurOnSubmit } = this.state
    return (
      <View style={styles.textInput}>
        <View style={{ flex: 3 }}>
          <Text>TextInput:</Text>
          <TextInput
            style={{ height: 25 }}
            autoCapitalize={autoCapitalizeMode}
            onKeyPress={this.keyPressedHandler}
            blurOnSubmit={blurOnSubmit}>
          </TextInput>
          <Text>Pressed: {pressed}</Text>
        </View>
        <View style={{ flex: 1 }}>
          <Text style={styles.subCaption}>autoCapitalize</Text>
          {
            this.autoCapitalize.map((element, i) => {
              return (
                <TouchableOpacity
                  key={i}
                  style={element === autoCapitalizeMode ? styles.autoCapSelected : styles.autoCapOrdinar}
                  onPress={this.selectCapMode.bind(null, element)}>
                  <Text>{element}</Text>
                </TouchableOpacity>
              )
            })
          }

          <TouchableOpacity 
            style={blurOnSubmit ? styles.autoCapSelected : styles.autoCapOrdinar}
            onPress={this.toggleBlurOnSubmit}>
            <Text>BlurOnSubmit</Text>
          </TouchableOpacity>
        </View>
      </View>
    )
  }
}