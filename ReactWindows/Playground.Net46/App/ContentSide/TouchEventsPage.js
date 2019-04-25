import React, { Component } from 'react'
import PropTypes from 'prop-types'
import {
  Text,
  TextInput,
  TouchableOpacity,
  View,
  Image
} from 'react-native'
import styles from './styles'
import images from '../Images'

export default class TouchEvents extends Component {
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

  keyPressedHandler = () => {
    this.props.logger(`Touch`)
  }

  render() {
    return (
        <View style={{width: 100, height: 50}}>
        <TouchableOpacity
          style={{
            flex: 1,
            height: '100%',
            borderBottomLeftRadius: 22,
            borderTopLeftRadius: 22,
            backgroundColor: '#0070A0'
          }}
          onPress={() => this.keyPressedHandler()}
          >
          <View pointerEvents={'box-only'}
            style={{
              flex: 1,
              height: '100%',
              borderBottomLeftRadius: 22,
              borderTopLeftRadius: 22,
              alignItems: 'center',
              justifyContent: 'center',
              backgroundColor: 'green',
            }}
              >
            <Text>JOIN</Text>
          </View>
        </TouchableOpacity>
      </View>
    )
  }
}