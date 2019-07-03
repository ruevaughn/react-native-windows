import React, { Component } from 'react'
import CommonModalStyles from './styles'
import { View, Text, TouchableOpacity, Image, Button  } from 'react-native';
import images from "../Images";
import PropTypes from 'prop-types'

export default class GenericModal extends Component {
  static propTypes = {
    isOpen: PropTypes.bool,
    close: PropTypes.func,
  }


  render () {
    if (!this.props.isOpen) return null

    return (
      <View style={CommonModalStyles.wrapper}>
        <View style={CommonModalStyles.backdrop} />
        <View style={CommonModalStyles.modalWrapper}>
          <View isFocusable={true} accessibilityLabel={'About window'} style={[CommonModalStyles.modal]}>
            <View style={CommonModalStyles.titleWrapper}>
              <Text selectable={true} accessibilityLabel={'BlueJeans title'} style={CommonModalStyles.title}>BlueJeans</Text>
            </View>
            <View style={CommonModalStyles.separator} />
            <View style={CommonModalStyles.content}>
              <Image isFocusable={true} accessibilityLabel={'Logo'} style={CommonModalStyles.imageBox} source={images.bjnLogo} />
              <Text selectable={true} accessibilityLabel={'BlueJeans version'} style={CommonModalStyles.version}>0.0.0.0</Text>
              <Text selectable={true} accessibilityLabel={'BlueJeans copyright'} style={CommonModalStyles.copyright}>BlueJeans 2019</Text>
              <View style={{backgroundColor: 'red'}} isFocusable={true}>
                <TouchableOpacity onPress={() => this.props.close()}>
                  <Text>Close Modal</Text>
                </TouchableOpacity>
              </View>
            </View>
          </View>
        </View>
      </View>
    )
  }
}
